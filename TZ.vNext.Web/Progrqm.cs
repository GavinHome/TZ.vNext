using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TZ.vNext.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //LoadLog4netConfig();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigConfiguration)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //logging.AddProvider(new Log4NetExtensions.Log4netProvider("log4net.config"));
                })
                .Build();

        private static void ConfigConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder config)
        {
            config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.Development.Mac.json", optional: true, reloadOnChange: true);
                ////.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                ////.AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        }
    }
}