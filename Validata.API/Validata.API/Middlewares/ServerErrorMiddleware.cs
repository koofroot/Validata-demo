using Validata.Infrastructure.Exceptions;

namespace Validata.API.Middlewares
{
    public class ServerErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ServerErrorMiddleware(RequestDelegate request)
        {
            _next = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (NotFoundException)
            {
                context.Response.StatusCode = 404;
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = 500;
            }
        }
    }
}
