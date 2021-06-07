namespace CreateManga.Application.Infrastructure.Middlewares
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class GlobalMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
               await next(httpContext);
            }
            catch (Exception)
            {
                httpContext.Response.Redirect("/Home/Error");
            }
        }
    }
}
