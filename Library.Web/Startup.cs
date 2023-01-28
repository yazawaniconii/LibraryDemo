using System;
using Library.Web.Dal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine(_env.EnvironmentName);
            string connectionString;
            if (_env.IsDevelopment())
            {
                connectionString = Configuration.GetConnectionString("mysql");
                Console.WriteLine(connectionString);
            }
            else
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.secrets.json");
                var configuration = config.Build();
                connectionString = configuration.GetConnectionString("mysql");
            }

            services.AddDbContext<LibDbContext>(options =>
                options
                    .UseLoggerFactory(LibDbContext.MyLoggerFactory)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .UseMySql(connectionString,
                        new MySqlServerVersion(new Version(8, 0))));


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    options.SlidingExpiration = true;
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.LogoutPath = "/Account/Logout";
                });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            var pathBase = Configuration["ASPNETCORE_PATH_BASE"];
            Console.WriteLine("PathBase: " + pathBase);
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}