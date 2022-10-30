using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace T104.Store.AdminServer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            /*
              IHostBuilder hostBuilder = CreateHostBuilder(args);
              IHost host = hostBuilder.Build();
              host.Run(); 
            */

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}

