using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using webapp.Data;
using webapp.Models;

namespace webapp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Called by the runtime. Add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Cookie Consent
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".MVC.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(5);  // Set a short timeout for easy testing.
                options.Cookie.HttpOnly = true;
            });

            // DB
            services.AddDbContext<MvcDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("MvcDbConnection")));

            // Identity
            services.AddIdentity<webapp.Models.AppUser, webapp.Models.AppRole>()
                .AddRoles<webapp.Models.AppRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<MvcDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 3;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyz0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.SlidingExpiration = true;

                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin",
                     policy => policy.RequireRole("Administrator"));
                options.AddPolicy("IsEdit",
                     policy => policy.RequireRole("Administrator", "Editor"));
            });

            // MVC
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddSessionStateTempDataProvider()
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            // Misc    
            services.AddSingleton<IEmailSender, EmailSender>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();  // see https://aka.ms/aspnetcore-hsts
            }

            // https://docs.microsoft.com/en-us/dotnet/standard/globalization-localization/globalization
            CultureInfo enUsCulture = new CultureInfo("en-US", false);
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = enUsCulture;

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                // dedicated routes
                //routes.MapRoute("home_about", "about",
                //    defaults: new { controller = "Home", action = "About" });
                //routes.MapRoute("home_privacy", "privacy",
                //    defaults: new { controller = "Home", action = "Privacy" });
            });

        }

    }
}
