using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using T109.ActiveDive.Core;
using T109.ActiveDive.DataAccess.DataAccess;
using T109.ActiveDive.FrontEnd.Blazor.Data;

namespace T109.ActiveDive.FrontEnd.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            Serilog.ILogger logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
            .Enrich.FromLogContext()
            .WriteTo.BrowserConsole()
            .CreateLogger();

            builder.Services.AddSingleton(typeof(Serilog.ILogger), (x) => logger);

            /*
            builder.Services.AddSingleton(typeof(Serilog.ILogger), (x) => new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
            .Enrich.FromLogContext()
            //.WriteTo.Http(requestUri: "https://logging.ricompany.info/api/v1/log-events")
            .CreateLogger());

            */


            builder.Services.AddScoped(typeof(WebApiAsyncRepository<ActiveDiveEvent>), (x) => new WebApiAsyncRepository<ActiveDiveEvent>(logger)
                                                                                                            .SetGetAllHostPath("api/eventdata/getall/")
                                                                                                            .SetSearchHostPath("api/eventdata/search/")
                                                                                                            .SetGetByIdOrNullHostPath("api/eventdata/GetByIdOrNull/")
                                                                                                            .SetBaseAddress("https://activediveeventapi.t109.tech"));

           builder.Services.AddScoped(typeof(StoreManager), typeof(StoreManager));


            builder.Services.AddScoped(typeof(ComponentHub), (x) => new ComponentHub());

            builder.Services.AddSingleton(typeof(FrontEndSettings), (x) => new FrontEndSettings(false, false, false));

            await builder.Build().RunAsync();

        }
    }
}
