using System;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Intermail.Dto;

namespace Intermail.Middleware
{
	public static class ApplicationExceptionHandlerMiddleware
	{
		public static async Task HandleException(HttpContext context)
		{
            var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptionFeature != null)
            {
                var exception = exceptionFeature.Error;
                string message = exception.Message;

                if (exception is BadRequestException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

                else if(exception is NotFoundException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                else
                {
                    message = "Internal server error";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                var result = new
                {
                    error = message,
                };

                await context.Response.WriteAsJsonAsync(result);
            }
        }
	}
}

