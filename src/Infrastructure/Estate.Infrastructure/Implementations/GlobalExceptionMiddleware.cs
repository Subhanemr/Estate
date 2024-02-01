using Microsoft.AspNetCore.Http;

namespace Estate.Infrastructure.Implementations
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                context.Response.Redirect($"/Home/ErrorPage?error={e.Message}");
            }
        }
    }
}
