using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using CustomMiddleware.Web.Middleware;

namespace CustomMiddleware.Web
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<AuthenticationMiddleware>();
            
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Authorized.");
            });
        }
    }
}
