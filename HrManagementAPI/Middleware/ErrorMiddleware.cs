using Newtonsoft.Json;
using System;
using System.Net;

namespace HrManagementAPI.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;

            if (isDevelopment)
            {
                if (ex.InnerException != null)
                    return context.Response.WriteAsync(ex.InnerException.Message);

                return context.Response.WriteAsync(ex.Message);
            }

            return context.Response.WriteAsync(context.Response.StatusCode + " Internal Server Error.");
        }
    }
}
