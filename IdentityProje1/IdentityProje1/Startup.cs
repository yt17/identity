using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityProje1.CustomValidations;
using IdentityProje1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityProje1
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
            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:Connection1"]);
            });




            services.AddIdentity<AppUser, AppRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";


                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;

            })
                .AddUserValidator<UserValidator>()
                .AddPasswordValidator<PasswordValidator>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
                


            CookieBuilder cookieBuilder = new CookieBuilder();

            cookieBuilder.Name = "website";
            cookieBuilder.HttpOnly = false;
           // cookieBuilder.Expiration = System.TimeSpan.FromDays(60);
            cookieBuilder.SameSite = SameSiteMode.Lax;
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            

            services.ConfigureApplicationCookie(opts => {
                opts.LoginPath = new PathString("/Home/Login");
                opts.LogoutPath = new PathString("/Member/Logout");
                
                opts.Cookie = cookieBuilder;
                opts.ExpireTimeSpan= System.TimeSpan.FromDays(60);
                opts.SlidingExpiration = false;
                opts.AccessDeniedPath = new PathString("/Member/AccessDenied");
            });

            //            services.AddControllersWithViews();
            services.AddMvc();
             
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();

            //}
    
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            //app.UseMvcWithDefaultRoute();
            // app.UseCors();

            //app.UseRouting();
            //app.UseMvcWithDefaultRoute();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
