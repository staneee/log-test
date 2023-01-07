using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
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
        /// 配置 SerilogLogger 配置
        /// </summary>
        /// <returns></returns>
        private static Serilog.ILogger CreateSerilogLogger()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            // 获取日志配置文件
            var configuration = ConfigurationHelper.GetConfiguration("serilogs", environmentName: env);
            var cfg = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration);

            return cfg.CreateLogger();
        }
    }
}
