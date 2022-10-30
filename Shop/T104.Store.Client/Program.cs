using BBComponents.Services;
using T104.Store.AdminClient;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using T104.Store.Engine;
using T104.Store.DataAccess.DataAccess;
using T104.Store.Engine.Models;
using T104.Store.Engine.DbContexts;
using T104.Store.Service.Metamodel;
using T104.Store.Engine.MetaModels;
using Serilog;
using Serilog.Events;


namespace T104.Store.AdminClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("BlazorCMS.Server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
               .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorCMS.Server"));

            builder.Services.AddScoped<IMetaModel, T104ShopMetaModel>();

            builder.Services.AddSingleton(typeof(Serilog.ILogger), (x) => new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                        .Enrich.FromLogContext()
                        .WriteTo.BrowserConsole()
                        .CreateLogger());
                        


            //Console.WriteLine("Murzik 01");

            //HttpClient httpClient = builder.Services.BuildServiceProvider().GetService<HttpClient>();

            //builder.Services.AddScoped(typeof(ShopSettingsManager), (x) => new ShopSettingsManager(new WebApiAsyncRepository<ShopSetting>(_httpClient, "ShopSettings/")));

            // Service to add alerts

            builder.Services.AddScoped<IAlertService, AlertService>();

            builder.Services.AddOidcAuthentication(options =>
            {
                //options.ProviderOptions.Authority = "https://localhost:10001";
                options.ProviderOptions.Authority = "https://auth.ricompany.info";
                options.ProviderOptions.ClientId = "client_blazor";
                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.DefaultScopes.Add("profile");
                options.ProviderOptions.DefaultScopes.Add("openid");
                options.ProviderOptions.DefaultScopes.Add("Blazor");
                options.ProviderOptions.DefaultScopes.Add("ServerAPI");
                

               // options.UserOptions.NameClaim = "preferred_username";
            });

//            Console.WriteLine("Murzik 02");

            await builder.Build().RunAsync();
        }
    }
}
