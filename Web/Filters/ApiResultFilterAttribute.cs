using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            switch (context.Result)
            {
                case OkObjectResult okObjectResult:
                {
                    var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, okObjectResult.Value);
                    context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
                    break;
                }
                case OkResult okResult:
                {
                    var apiResult = new ApiResult(true, ApiResultStatusCode.Success);
                    context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
                    break;
                }
                case CreatedAtActionResult createdAtResult:
                {
                    var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, createdAtResult.Value);
                    context.Result = new CreatedAtActionResult(
                            createdAtResult.ActionName,
                            createdAtResult.ControllerName,
                            createdAtResult.RouteValues,
                            apiResult)
                        { StatusCode = createdAtResult.StatusCode };
                    break;
                }
                case ObjectResult { StatusCode: 400 } badRequestObjectResult:
                {
                    string? message = null;
                    switch (badRequestObjectResult.Value)
                    {
                        case ValidationProblemDetails validationProblemDetails:
                            var errorMessages = validationProblemDetails.Errors.SelectMany(p => p.Value).Distinct();
                            message = string.Join(" | ", errorMessages);
                            break;
                        case SerializableError errors:
                            var errorMessages2 = errors.SelectMany(p => (string[])p.Value).Distinct();
                            message = string.Join(" | ", errorMessages2);
                            break;
                        case not null and not ProblemDetails:
                            message = badRequestObjectResult.Value?.ToString();
                            break;
                    }
                    var apiResult = new ApiResult(false, ApiResultStatusCode.BadRequest, message);
                    context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
                    break;
                }
                case ObjectResult { StatusCode: 404 } notFoundObjectResult:
                {
                    string? message = null;
                    if (notFoundObjectResult.Value != null && notFoundObjectResult.Value is not ProblemDetails)
                        message = notFoundObjectResult.Value.ToString();
                    var apiResult = new ApiResult(false, ApiResultStatusCode.NotFound, message);
                    context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
                    break;
                }
                case ContentResult contentResult:
                {
                    var apiResult = new ApiResult(true, ApiResultStatusCode.Success, contentResult.Content);
                    context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
                    break;
                }
                case ObjectResult { StatusCode: null, Value: not ApiResult } objectResult:
                {
                    var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, objectResult.Value);
                    context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
                    break;
                }
            }

            await base.OnResultExecutionAsync(context, next);
        }
    }
}