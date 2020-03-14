using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AltezaWebApp.Filters;
using AltezaWebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AltezaWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            })
            .AddCookie(options => {
                options.SlidingExpiration = true;
                options.LoginPath = "/ControlPanel/Login/"; // auth redirect
                options.ExpireTimeSpan = new TimeSpan(0, 0, 10, 0);
                options.ReturnUrlParameter = "returnurl";
                options.Events.OnRedirectToLogin = (context) =>
                {
                    context.Response.StatusCode = 401;
                    return Task.FromResult(0);
                };
            });

            services.AddMvc();
            //services.Configure<CookieAuthenticationOptions>(options =>
            //{
            //    options.LoginPath = new PathString("/ControlPanel/Login");
            //    options.AccessDeniedPath = new PathString("/Error/PageNotFound");
               
            //});
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "Admin");
                });
            });
            services.AddTransient<IDbConnection>((sp) =>
                    new SqlConnection(this.Configuration.GetConnectionString("ConnString"))
                );
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new CheckUserLoginImpls());
            //});
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/ControlPanel/Login");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/InternalServerError");
            }
            app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");
            //app.Use(async (ctx, next) =>
            //{
            //    await next();

            //    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
            //    {
            //        //Re-execute the request so the user gets the error page
            //        string originalPath = ctx.Request.Path.Value;
            //        ctx.Items["originalPath"] = originalPath;
            //        ctx.Request.Path = "/Error/PageNotFound";
            //        await next();
            //    }
            //});
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
