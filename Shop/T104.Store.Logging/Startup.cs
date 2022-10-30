using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T104.Store.DataAccess;
using T104.Store.DataAccess.Abstract;
using T104.Store.Logging.Models;
using T104.Store.Engine.DbContexts;
using Microsoft.OpenApi.Models;

namespace T104.Store.Logging
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


            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "T104.Store.AssortmentWebApi", Version = "v1" });
            });

            LoggerEfSqlServerDbContext myDbContext = new LoggerEfSqlServerDbContext();

            myDbContext.Database.EnsureCreated();

            services.AddScoped(typeof(IAsyncRepository<LoggerMessage>), (x) => new EfAsyncRepository<LoggerMessage>(myDbContext));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            if (env.IsDevelopment())
            {
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
