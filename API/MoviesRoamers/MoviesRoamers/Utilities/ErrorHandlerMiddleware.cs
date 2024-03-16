using System.Net;
using System.Text.Json;

namespace MoviesRoamers.Utilities
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {

                    case ArgumentOutOfRangeException:
                    case ArgumentException:
                    case HttpRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ExecutionEngineException e:
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;
                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                }
                dynamic errors;
                try
                {
                    errors = JsonSerializer.Deserialize<dynamic>(error?.Message);
                }
                catch (Exception ex)
                {
                    errors = new List<string>();
                    errors.Add(error?.Message);
                }

                var result = JsonSerializer.Serialize(new { succes = false, errors = errors });
                await response.WriteAsync(result);
            }
        }
    }
}
