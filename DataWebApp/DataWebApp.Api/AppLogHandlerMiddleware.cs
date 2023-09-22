using DataWebApp.Api.Constants;
using DataWebApp.Api.Exceptions;
using DataWebApp.Api.Util;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Text;

namespace DataWebApp.Api
{
    public class AppLogHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IActionResultExecutor<ObjectResult> executor;

        private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();
        public AppLogHandlerMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> executor)
        {
            this.next = next;
            this.executor = executor;
        }
        public async Task InvokeAsync(HttpContext context, ILogger<AppLogHandlerMiddleware> logger)
        {
            var request = context.Request;
            var url = context.Request.Path.Value;
            var displayUrl = context.Request.GetDisplayUrl();
            string userId = context.User.Identity.Name ?? "Not-Login";

            try
            {
                logger.LogInformation($"Started : {url}");
                await next(context);
                logger.LogInformation($"Completed : {url}");
            }
            catch (UnauthorizedException ex)
            {
                logger.LogError(ex, "Unauthorized Error Occured");
                await ExecuteAsync(executor, context, HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception has occurred while executing the rquest");
                await ExecuteAsync(executor, context, HttpStatusCode.InternalServerError);
            }
        }

        private async Task ExecuteAsync(IActionResultExecutor<ObjectResult> executor, HttpContext context, HttpStatusCode statusCode)
        {
            var routeData = context.GetRouteData() ?? new RouteData();
            var actionContext = new ActionContext(context, routeData, EmptyActionDescriptor);
            var result = BuildResponsObj(statusCode);
            await executor.ExecuteAsync(actionContext, result);
        }
        private static ObjectResult BuildResponsObj(HttpStatusCode httpStatusCode)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            apiResponse.Payload = null;
            apiResponse.IsSuccess = false;
            apiResponse.Message = ErrorMessages.UNHANDLED_EXCEPTION;

            var result = new ObjectResult(apiResponse)
            {
                StatusCode = (int)httpStatusCode
            };
            return result;
        }

        private static string GetRequestData(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            if (context.Request.HasFormContentType && context.Request.Form.Any())
            {
                sb.Append("Form variables: ");
                foreach (var x in context.Request.Form)
                {
                    sb.AppendFormat("Key={0}, Value={1}<br/>", x.Key, x.Value);
                }
            }
            sb.AppendLine("Method: " + context.Request.Method);
            return sb.ToString();
        }
    }
}
