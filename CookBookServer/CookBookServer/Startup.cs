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

namespace CookBookServer
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
            string mongoConnStr = Configuration["MongoDbConnectionString"];
            services.AddScoped<IMongoClient, MongoClient>(c => new MongoClient(mongoConnStr));
            services.Configure<MongoDbOptions>(Configuration.GetSection("MongoAuthorizedDbOptions"));

            services.AddScoped<AuthRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<CookieProvider>();

            services.AddControllersWithViews();
            services.AddMapper("CookBookServer");
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
            app.UseStaticFiles();

            app.UseRouting();

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
