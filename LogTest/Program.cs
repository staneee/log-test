using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace LogTest
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = CreateSerilogLogger();
            try
            {
                Log.Information("Configuing ({ApplicationName})...");

                var host = CreateHostBuilder(args);

                Log.Information("Starting ({ApplicationName})...");

                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationName})!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHost CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog()
                .Build()
            ;
        }


        /// <summary>
        /// ���� SerilogLogger ����
        /// </summary>
        /// <returns></returns>
        private static Serilog.ILogger CreateSerilogLogger()
        {
            var currentAssemblyPath = typeof(Program).Assembly.Location;
            var path = Path.GetDirectoryName(currentAssemblyPath);

            // ��̬����dll
            LoadAssembly(Path.Join(path, "System.Text.Json.dll"));
            LoadAssembly(Path.Join(path, "Serilog.Sinks.Grafana.Loki.dll"));


            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            // ��ȡ��־�����ļ�
            var configuration = ConfigurationHelper.GetConfiguration("serilogs", environmentName: env);
            var cfg = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration);

            return cfg.CreateLogger();
        }


        private static Assembly LoadAssembly(string filePath)
        {
            // �ο�:
            // https://stackoverflow.com/questions/63616618/how-to-dynamically-load-and-unload-reload-a-dll-assembly
            // https://learn.microsoft.com/en-us/dotnet/api/system.runtime.loader.assemblyloadcontext?view=net-5.0

            if (File.Exists(filePath))
            {
                return AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
            }

            return null;
        }
    }
}
