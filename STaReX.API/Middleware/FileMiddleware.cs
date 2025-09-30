using System.Net;

namespace STaReX.API.Middleware
{
    public class FileMiddleware
    {
        private readonly RequestDelegate _next;

        public FileMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        { 
            if(context.Request.Path.StartsWithSegments("/private"))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Unauthorized!");
                return;
            }

            await _next(context);
        
        }
    }
}
