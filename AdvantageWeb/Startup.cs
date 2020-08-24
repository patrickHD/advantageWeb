using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using AdvantageWeb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using AdvantageWeb.Models;

namespace AdvantageWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<string> allowedDomains = new List<string>() { "coegipartners.com", "radar-analytics.com", "coegiweb.com" };
        public IConfiguration Configuration { get; }
        public UserManager<IdentityUser> UserManager;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>options.UseNpgsql(Configuration["AppConnection"]));
            services.AddDbContext<staticdbContext>(options => options.UseNpgsql(Configuration["TMConnection"]));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();
            UserManager = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/AdvantageTool");
            });
            services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.OnAppendCookie = cookieContext =>
                {
                    if (cookieContext.CookieOptions.SameSite == SameSiteMode.None)
                    {
                        var userAgent = cookieContext.Context.Request.Headers["User-Agent"].ToString();
                    }
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    if (!context.Context.User.Identity.IsAuthenticated && context.Context.Request.Path.StartsWithSegments("/fileResults"))
                    {
                        context.Context.Abort();
                        throw new Exception("Not authenticated");
                    }
                }
            });
            app.Use(async (context, next) =>
            {
                _ = context;
                if (context.User.Identity.IsAuthenticated)
                {
                    if (!allowedDomains.Contains(context.User.Identity.Name.Split('@')[1]))
                    {
                        var user = context.User;
                        //var userManager = context.RequestServices.GetService(UserManager<IdentityUser>);
                        await context.SignOutAsync(IdentityConstants.ApplicationScheme);
                        await UserManager.DeleteAsync(await UserManager.GetUserAsync(user));
                        await context.Response.WriteAsync("Unauthorized. Domain is not allowed. Refresh to go home.");
                    }
                }
                await next();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
