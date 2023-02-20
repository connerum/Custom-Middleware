using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CustomMiddleware.Web.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string username = context.Request.Query["username"];
            string password = context.Request.Query["password"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || username != "user1" || password != "password1")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Not authorized.");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
