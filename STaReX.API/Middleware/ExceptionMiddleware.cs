using System.Net;
using STaReX.ENTITY.Dto;

namespace STaReX.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                switch (context.Response.StatusCode)
                {

                    case (int)HttpStatusCode.Unauthorized:

                        var response = StatusResponse<NoData>.Fail("Unauthorized!");
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await context.Response.WriteAsJsonAsync(response);
                        break;
                }
            }

            catch (UnauthorizedAccessException ex)
            {
                var response = StatusResponse<NoData>.Fail(ex.Message, statusCode: HttpStatusCode.Unauthorized);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(response);
            }

            catch (Exception ex)
            {
                var response = StatusResponse<NoData>.Fail(ex.Message, statusCode: HttpStatusCode.InternalServerError);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(response);
            }

        }
    }
}
