﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PerformanceLogger;
using PerformanceLogger.Extensions.Logging;
using PerformanceLogger.Extensions.Postgres;
using PerformanceLogger.Extensions.DependencyInjection;
using System.Threading;
using Sample.SampleServices;

namespace Sample
{
    /// <summary>
    /// This is a sample console application demonstrating how to use the Performance Logger to track operation
    /// execution times, using the Logging extension (PerformanceLogger.Extensions.Logging).
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Instanciate a console logger, implementing ILogger. Will be used to show logs on console.
            var provider = new ServiceCollection()
                .AddLogging(builder => {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Debug);
                })
                .AddPerformanceLogger(config => {
                    config.AddLogging();
                    config.AddPostgres("Host=localhost;Username=postgres;Password=ADMINVME;Database=performancelogs", "logs");
                    config.AutoLogPerformance(typeof(IService));
                })
                .AddTransient<IService, ServiceB>()
                .AddTransient<ServiceA>()
                .AddTransient<ServiceB>()
                .BuildServiceProvider();

            var consoleLogger = provider.GetService<ILogger<Program>>();
            consoleLogger.LogInformation("Application start");

            var serviceA = provider.GetService<ServiceA>();
            var serviceB = provider.GetService<ServiceB>();

            serviceA.ExecuteSomething();
            serviceB.ExecuteSomething();

            // Causes the loggers to flush their logs, and the targets to end their parallel tasks
            provider.Dispose();
        }
    }
}
