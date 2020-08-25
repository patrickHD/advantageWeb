using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AdvantageWeb.Classes
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BlockExternalUsers
    {
        private readonly RequestDelegate _next;
        //private readonly UserManager<IdentityUser> _userManager;
        public List<string> allowedDomains = new List<string>() { "coegipartners.com", "radar-analytics.com", "coegiweb.com" };

        public BlockExternalUsers(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<IdentityUser> userManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                if (!allowedDomains.Contains(context.User.Identity.Name.Split('@')[1]))
                {
                    var user = context.User;
                    await context.SignOutAsync(IdentityConstants.ApplicationScheme);
                    await userManager.DeleteAsync(await userManager.GetUserAsync(user));
                    await context.Response.WriteAsync("Unauthorized. Domain is not allowed. Refresh to go home.");
                }
            }
            await _next.Invoke(context);
        }
    }

    //// Extension method used to add the middleware to the HTTP request pipeline.
    //public static class BlockExternalUsersExtensions
    //{
    //    public static IApplicationBuilder UseBlockExternalUsers(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<BlockExternalUsers>();
    //    }
    //}
}
