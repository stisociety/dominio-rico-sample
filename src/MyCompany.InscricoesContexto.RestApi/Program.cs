using System;
using System.IO;
using System.Net;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace MyCompany.InscricoesContexto.RestApi
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();
            Log.Logger = CreateSerilogLogger(configuration);
            try
            {
                var host = BuildWebHost(configuration, args);
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables();
            return builder.Build();
        }

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            /*var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
            var key = configuration["ApplicationInsights:InstrumentationKey"];
            if (string.IsNullOrEmpty(key))
                key = configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
            if (string.IsNullOrEmpty(key))
                key = configuration.GetConnectionString("ApplicationInsights");
            telemetryConfiguration.InstrumentationKey = key;*/

            var builder = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Async(a => a.Console());
                // .WriteTo.Async(a => a.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces));
                
            return builder.CreateLogger();
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false)
                .ConfigureKestrel(options =>
                {
                    var port = configuration.GetValue("Port", 80);
                    options.Listen(IPAddress.Any, port, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    });
                })
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSerilog()
                .Build();
    }
}