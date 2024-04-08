using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace BookServiceAPP.API.Custom
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Unable to retrive Books at the moment";

            if (exception is ValidationException validationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = string.Join(", ", validationException.Errors.Select(x => x.ErrorMessage));
            }

            var response = new { message = message };
            var payload = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
        }
    }
}
