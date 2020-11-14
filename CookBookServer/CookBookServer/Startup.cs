using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using CookBookServer.Repositories;
using CookBookServer.Models;
using CookBookServer.Providers;
using CookBookServer.Code.Automapper;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CookBookServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string mongoConnStr = Configuration["MongoDbConnectionString"];
            services.AddScoped<IMongoClient, MongoClient>(c => new MongoClient(mongoConnStr));
            services.Configure<MongoDbOptions>(Configuration.GetSection("MongoAuthorizedDbOptions"));

            services.AddScoped<AuthRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<CookieProvider>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)               
               .AddCookie(options =>
                {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Auth/SignIn");
               });

            services.AddControllersWithViews();
            services.AddMapper("CookBookServer");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
