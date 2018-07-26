namespace SimpleChat
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using NLog;
    using NLog.Web;
    using System;
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            SetLogsPathAndReloadNlogConfig();
            CreateWebHostBuilder(args).Build().Run();
        }

        private static void SetLogsPathAndReloadNlogConfig()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var path = 
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}{config["RelativePaths:LogsRelativePath"]}";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            LogManager.Configuration.Variables["logsDirectory"] = path;
               

            LogManager.ReconfigExistingLoggers();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog();
    }
}
