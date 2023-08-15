using System;
using System.Net;
using Intermail.Services;

namespace Intermail.Middleware
{
	public class AuthMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IAppConfiguration _appConfig;


        public AuthMiddleware(IAppConfiguration appConfig, RequestDelegate next)
        {
            _appConfig = appConfig;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue(_appConfig.ApiTokenHeaderName, out var extractedKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("No Api key provided.");
                return;
            }

            if (!_appConfig.AppToken.Equals(extractedKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("No Api key provided.");
                return;
            }

            await _next(context);
        }
    }
}

