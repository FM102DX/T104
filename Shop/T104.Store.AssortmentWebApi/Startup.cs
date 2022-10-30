using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T104.Store.AssortmentWebApiData;

namespace T104.Store.AssortmentWebApi
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

            #region Logger

            Log.Logger = new LoggerConfiguration()
                                    .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                                    .Enrich.FromLogContext()
                                    .WriteTo.File("T104.Store.AssortmentWebApi.Startup.Logs.txt")
                                    .CreateLogger();

            Log.Logger.Information("Point 1");

            services.AddSingleton(typeof(Serilog.ILogger), (x) => new LoggerConfiguration()
                                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                    .Enrich.FromLogContext()
                                    .WriteTo.File("BarsikLogs1.txt")
                                    .CreateBootstrapLogger());
            Log.Logger.Information("Point 2");

            #endregion

            string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://t104shop.ricompany.info",
                                                          "https://t104assort.ricompany.info",
                                                          "https://localhost:44304");
                                  });
            });

            services.AddSingleton(typeof(StoreSkuInMemoryManager), (x) => new StoreSkuInMemoryManager());

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "T104.Store.AssortmentWebApi", Version = "v1" });
            });

            Log.Logger.Information("Point 9 end");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            if (env.IsDevelopment())
            {

            }
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "T104.Store.AssortmentWebApi v1"));

            app.UseStaticFiles();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
