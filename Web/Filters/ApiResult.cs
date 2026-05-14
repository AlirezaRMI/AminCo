using Domain.Common;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Web.Filters
{
    public record ApiResult(
        bool IsSuccess,
        ApiResultStatusCode StatusCode,
        string? JsonValidationMessage = null,
        string? Message = "")
    {
        public static implicit operator ApiResult(OkResult result)
            => new(true, ApiResultStatusCode.Success);
        public static implicit operator ApiResult(JsonResult result)
            => new(true, ApiResultStatusCode.Success);
        public static implicit operator ApiResult(BadRequestResult result)
            => new(false, ApiResultStatusCode.BadRequest);
        public static implicit operator ApiResult(ContentResult result)
            => new(true, ApiResultStatusCode.Success, result.Content);
        public static implicit operator ApiResult(NotFoundResult result)
            => new(false, ApiResultStatusCode.NotFound);
        public static implicit operator ApiResult(AppException result)
            => new(false, result.ApiStatusCode, Message: result.Message);
        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new(false, ApiResultStatusCode.BadRequest, message);
        }
        public static implicit operator ApiResult(Exception ex)
            => ex switch
            {
                DbUpdateException { InnerException: SqlException sqlEx } when (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    => new(false, ApiResultStatusCode.BadRequest, Message: $"Duplicate key error: {sqlEx.Message}"),
                ExistsException existsEx => new(false, existsEx.ApiStatusCode, existsEx.Message),
                BadRequestException badReq => new(false, badReq.ApiStatusCode, badReq.Message),
                NotFoundException notFound => new(false, notFound.ApiStatusCode, notFound.Message),
                AppException appEx => new(false, appEx.ApiStatusCode, appEx.Message),
                _ => new(false, ApiResultStatusCode.ServerError, "An unexpected error occurred.")
            };
    }

    public record ApiResult<TData>(
        bool IsSuccess,
        ApiResultStatusCode StatusCode,
        TData? Data,
        string? JsonValidationMessage = null,
        string? Message = "") : ApiResult(IsSuccess, StatusCode, JsonValidationMessage, Message) where TData : class
    {
        public static implicit operator ApiResult<TData>(TData data)
            => new(true, ApiResultStatusCode.Success, data);
        public static implicit operator ApiResult<TData>(OkResult result)
            => new(true, ApiResultStatusCode.Success, null);
        public static implicit operator ApiResult<TData>(JsonResult result)
            => new(true, ApiResultStatusCode.Success, null);
        public static implicit operator ApiResult<TData>(OkObjectResult result)
            => new(true, ApiResultStatusCode.Success, (TData)result.Value!);
        public static implicit operator ApiResult<TData>(BadRequestResult result)
            => new(false, ApiResultStatusCode.BadRequest, null);
        public static implicit operator ApiResult<TData>(AppException result)
            => new(false, result.ApiStatusCode, (TData)result.AdditionalData, result.AdditionalData.ToString(), result.Message);
        public static implicit operator ApiResult<TData>(UnauthorizedResult result)
            => new(false, ApiResultStatusCode.Unauthorized, null);
        public static implicit operator ApiResult<TData>(ContentResult result)
            => new(true, ApiResultStatusCode.Success, null, result.Content);
        public static implicit operator ApiResult<TData>(NotFoundResult result)
            => new(false, ApiResultStatusCode.NotFound, null);
        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
            => new(false, ApiResultStatusCode.NotFound, (TData)result.Value!);
        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new(false, ApiResultStatusCode.BadRequest, null, message);
        }
        public static implicit operator ApiResult<TData>(Exception ex)
            => ex switch
            {
                DbUpdateException { InnerException: SqlException sqlEx } when (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    => new(false, ApiResultStatusCode.BadRequest, null, Message: $"Duplicate key error: {sqlEx.Message}"),
                ExistsException existsEx => new(false, existsEx.ApiStatusCode, null, existsEx.Message),
                BadRequestException badReq => new(false, badReq.ApiStatusCode, null, badReq.Message),
                NotFoundException notFound => new(false, notFound.ApiStatusCode, null, notFound.Message),
                AppException appEx => new(false, appEx.ApiStatusCode, (TData?)appEx.AdditionalData, appEx.Message),
                _ => new(false, ApiResultStatusCode.ServerError, null, "An unexpected error occurred.")
            };
    }
}