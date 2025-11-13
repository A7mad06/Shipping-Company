using Domain.Exceptions;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using System.Net;
using System.Security.Authentication;

namespace Shipping_Company.MiddleWares
{
    public class GlobalExceptionHandelingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandelingMiddleware> _logger;

        public GlobalExceptionHandelingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandelingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    await HandelNotFoundException(httpContext);
                }
            }
            catch (Exception excpetion)
            {
                _logger.LogError($"Somthing went wrong");
                await HandelExceptionAsync(httpContext, excpetion);
            }
        }

        private async Task HandelNotFoundException(HttpContext httpContext)
        {
            httpContext.Request.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound
                ,
                ErrorMessage = $"The Endpoint {httpContext.Request.Path} is not found"
            };
            await httpContext.Response.WriteAsync(response.ToString());
        }

        //Handel Exceptions
        public async Task HandelExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorDetails();

            switch (exception)
            {
                case InvalidCredentialsException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.StatusCode = httpContext.Response.StatusCode;
                    response.ErrorMessage = exception.Message;
                    break;

                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = httpContext.Response.StatusCode;
                    response.ErrorMessage = "Something went wrong";
                    break;
            }

            await httpContext.Response.WriteAsync(response.ToString());
        }
    }
}
