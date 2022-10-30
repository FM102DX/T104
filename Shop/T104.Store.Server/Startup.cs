using T104.Store.Engine.Abstract.Service;
using T104.Store.Engine.Environment;
using T104.Store.AdminServer.Data;
using T104.Store.AdminServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using T104.Store.Engine;
using T104.Store.Engine.Models;
using T104.Store.DataAccess.DataAccess;
using T104.Store.Engine.DbContexts;
using T104.Store.DataAccess.Abstract;
using T104.Store.Service.Metamodel;
using T104.Store.Engine.MetaModels;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using T104.Store.DataAccess;

namespace T104.Store.AdminServer
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

            #region Logger

            Log.Logger = new LoggerConfiguration()
                                    .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                                    .Enrich.FromLogContext()
                                    .WriteTo.File("BarsikLogs0.txt")
                                    .CreateLogger();

            Log.Logger.Information("Point 1");

            services.AddSingleton(typeof(Serilog.ILogger), (x) => new LoggerConfiguration()
                                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                    .Enrich.FromLogContext()
                                    .WriteTo.File("BarsikLogs1.txt")
                                    .CreateBootstrapLogger());

            #endregion

            /*
            var ic = Configuration.GetSection("MyCatSettings").GetChildren();
            var ic2 = Configuration.GetChildren();
            foreach (var ch0 in ic2)
            {
                Log.Logger.Information($"Key={ch0.Key} value= {ch0.Value}");
            }
            */

            #region AuthAndCors

            Log.Logger.Information("Point 2 AuthAndCors");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
                {
                   // config.Authority = "https://localhost:10001";
                    config.Authority =   "https://auth.ricompany.info";
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };

                    config.RequireHttpsMetadata = false;
                });

            services.AddCors(confg =>
               confg.AddPolicy("AllowAll",
                   p => p.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader()));



            #endregion

            #region MetaModel
            Log.Logger.Information("Point 3 MetaModel");
            services.AddScoped<IMetaModel, T104ShopMetaModel>();

            #endregion

            #region ShopSettings
            Log.Logger.Information("Point 4 ShopSettings");
            services.AddScoped(typeof(ShopSettingsManager), (x) => new ShopSettingsManager(new EfAsyncRepository<ShopSetting>(new EFSqliteDbContext())));
            services.AddScoped(typeof(IAsyncRepository<ShopSetting>), (x) => new EfAsyncRepository<ShopSetting>(new EFSqliteDbContext()));

            IAsyncRepository<ShopSetting> repo = new EfAsyncRepository<ShopSetting>(new EFSqliteDbContext());
            repo.InitAsync(false);
            ShopSettingsManager shopSettingsManager = new ShopSettingsManager(repo);
            shopSettingsManager.Init(true);



            #endregion

            #region Mongo

            Log.Logger.Information("Point 5 Mongo");

            services.Configure<MongoDatabaseSettings>(Configuration.GetSection(nameof(MongoDatabaseSettings)));

            services.AddSingleton<IMongoDatabaseSettings>(sp =>sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            services.AddSingleton(typeof(IMongoService<>), typeof(MongoService<>));

            #endregion

            #region MyCat

            Log.Logger.Information("Point 6 MyCat");

            services.Configure<MyCat>(Configuration.GetSection("MyCatSettings"));

            services.AddSingleton<IMyCat>(sp => sp.GetRequiredService<IOptions<MyCat>>().Value);

            #endregion

            #region Main
            
            Log.Logger.Information("Point 7 Main");
            
            services.AddTransient<IDbInitializer, DbInitializer>();

            //enabling the in memory cache 
            services.AddMemoryCache();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddOpenApiDocument(options =>
            {
                options.Title = "T104 API Doc";
                options.Version = "1.0";
            });

            #endregion

            Log.Logger.Information("Point 9");
        }

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer, Serilog.ILogger logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3(x =>
            {
                x.DocExpansion = "list";
            });

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

            dbInitializer.InitializeDb();
        }
    }
}
