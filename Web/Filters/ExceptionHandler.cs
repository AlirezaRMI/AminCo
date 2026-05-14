using System.Net.Mime;
using Domain.Common;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Web.Filters
{
    public class ExceptionHandler(ILogger<ExceptionHandler> logger) : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            logger.LogError(context.Exception, "Unhandled exception");

            object result = context.Exception switch
            {
                ExistsException existsException => new ApiResult(false, existsException.ApiStatusCode, Message: existsException.Message),
                BadRequestException badRequest => new ApiResult(false, badRequest.ApiStatusCode, Message: badRequest.Message),
                NotFoundException notFound => new ApiResult(false, notFound.ApiStatusCode, Message: notFound.Message),
                AppException appException => new ApiResult(false, appException.ApiStatusCode, Message: appException.Message),
                DbUpdateException {InnerException: SqlException sqlEx} when (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    => new ApiResult(false, ApiResultStatusCode.BadRequest, Message: "رکورد تکراری است."),
                _ => new ApiResult(false, ApiResultStatusCode.ServerError, Message: "خطای داخلی سرور رخ داده است.")
            };

            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            context.Result = new ObjectResult(result);
            await Task.CompletedTask;
        }
    }
}