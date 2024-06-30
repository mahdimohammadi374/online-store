
using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Web.Middlewares
{
    public class MiddlewareExceptionHandler
    {
        private readonly IWebHostEnvironment _env;

        private readonly ILoggerFactory _logger;
        private readonly RequestDelegate _next;
        public MiddlewareExceptionHandler(IWebHostEnvironment env, ILoggerFactory logger, RequestDelegate next)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); //here request is sent and if an error exist anywhere then cath will execute
            }
            catch (Exception ex)
            {
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                string result = HandleServerError(context, ex);

                result = HandleResult(context, ex, result, options);


                await context.Response.WriteAsync(result);
            }
        }

        private static string HandleServerError(HttpContext context, Exception ex)
        {
            context.Request.ContentType = "application/json";


            var result = JsonSerializer.Serialize(new ApiToReturn(500, ex.Message));
            return result;
        }

        private string HandleResult(HttpContext context, Exception ex, string result, JsonSerializerOptions options)
        {
            switch (ex)
            {
                case NotFoundEntityException notFoundEntityException:
                    if (ex.Message !=null) { }
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new ApiToReturn(404,
                        notFoundEntityException.Message,
                        notFoundEntityException.Messages, ex.Message), options);
                    break;

                case BadRequestEntityException badRequestEntityException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new ApiToReturn(400, badRequestEntityException.Message, badRequestEntityException.Messages, ex.Message), options);
                    break;

                case ValidationEntityException validationEntityException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new ApiToReturn(400, validationEntityException.Message, validationEntityException.Messages, ex.Message), options);
                    break;
            }

            return result;
        }
    }
}
