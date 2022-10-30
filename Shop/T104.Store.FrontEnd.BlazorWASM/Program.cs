using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using T104.Store.AdminClient.Data;
using T104.Store.FrontEnd.BlazorWASM.Data;

namespace T104.Store.FrontEnd.BlazorWASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            Serilog.ILogger logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
            .Enrich.FromLogContext()
            .WriteTo.BrowserConsole()
            .CreateLogger();

            builder.Services.AddSingleton(typeof(Serilog.ILogger), (x) => new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
            .Enrich.FromLogContext()
            .WriteTo.Http(requestUri: "https://logging.ricompany.info/api/v1/log-events")
            .CreateLogger());



            builder.Services.AddScoped(typeof(StoreSkuClientManager), (x) => new StoreSkuClientManager("https://t104assort.ricompany.info/", "api/v1/assortdemo", logger));

            builder.Services.AddScoped(typeof(StoreManager), (x) => new StoreManager(@"https://t104shop.ricompany.info/"));

            builder.Services.AddScoped(typeof(ComponentHub), (x) => new ComponentHub());
            

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
