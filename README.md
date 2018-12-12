[![NuGet](https://img.shields.io/nuget/v/PerformanceLogger.svg)](https://nuget.org/packages/PerformanceLogger)

PerformanceLogger is a tool for Microsoft .NET Standard 2.0 to help with logging the performance (execution times) of any .NET application at the method level. The library comes with a simple API and an easy-to-extend architecture allowing writing extensions to log to different targets. Support [Microsoft.Extensions.ILogger](https://github.com/aspnet/Extensions/tree/master/src/Logging) and Postgres database as logging targets. It is designed to have a low impact on performance, enabling detailed monitoring performance on live production systems. Used in Continuous Integration or local developement, it enables measuring the impacts of design decisions.

## Get Packages

Get the packages with NuGet.

To log to an ILogger `dotnet add package PerformanceLogger.Extensions.Logging`

To log to an Postgres database `dotnet add package PerformanceLogger.Extensions.Postgres`

To extend and implement your own targets `dotnet add package PerformanceLogger`

## Get Started

See our [Sample Project](https://github.com/corentinaltepe/PerformanceLogger/tree/master/Sample) for an example of usage of the PerformanceLogger with an ILogger and a Postgres database, in a console application.

### Logging to an ILogger

```csharp
// Get an instance of your ILogger
var logger = serviceProvider.GetService<ILogger>();

// Build a performance logger that will log into the ILogger
var performanceLogger = new PerformanceLoggerBuilder()
    .AddLogger(logger)
    .Build();


// Start tracking a long operation
var perfTracker = performanceLogger.Start("my_long_task_01");

// Simulating a long operation
Thread.Sleep(25);

// End the tracking (and log the results)
perfTracker.End();
```

In the case of a console logger, the results will look like this :

[logo]: https://github.com/corentinaltepe/PerformanceLogger/tree/master/Documentation/images/001.PNG "Results of performance log on a console logger"

You are free to setup an ILogger with [NLog](https://github.com/NLog/NLog.Extensions.Logging), [log4net](http://logging.apache.org/log4net/), [Serilog](https://github.com/serilog/serilog-extensions-logging) or any other adapter.

### Logging to a Postgres database

All the performance logs go to a dedicated table of which you can select the name, and defaults to `perflogs`. The table will be created if it does not already exists.

```csharp
var connectionString = "Host=localhost;Username=postgres;Password=MYPASSWORD;Database=performancelogs";
var tableName = "perflogs";

// Build a performance logger that will log into a Postgres database
var performanceLogger = new PerformanceLoggerBuilder()
    .AddPostgres(connectionString, tableName)
    .Build();
```

The PostresTarget writes the logs to the database by batches, in a parallel Task for a lower performance impact. Note that you must properly `Dispose()` the `performanceLogger` (blocking call) to ensure not losing the last logs in the queue when exiting an application.

### Multiple Targets

As many targets as needed can be used simultaneously to log the application performance with the builder, as follows

```csharp
// Build a performance logger that will log into two databases, and one ILogger
var performanceLogger = new PerformanceLoggerBuilder()
    .AddPostgres(connectionString1, "logs")
    .AddPostgres(connectionString2, "perflogs")
    .AddLogger(logger1)
    .Build();
```

### Dependency Injection

TODO: extend Microsoft's dependency injection mechanism

## Extend

It is possible to extend the base package and write your own target for the performance logs. Write your own implementation of `ITarget` as follows

```csharp
// Implementation of performance logging target which
// writes the logs into the Console with Console.WriteLine()
public class MyConsoleTarget : ITarget
{
    public void Log(PerformanceResult report)
    {
        // Format the entry
        var entry = $"{report.StartDate.ToShortDateString()} " +
        $"{report.StartDate.ToShortTimeString()};" +
        $"{report.EventId};{report.Duration.TotalMilliseconds} ms;";

        // Log it
        Console.WriteLine(entry);
    }

    public void Dispose() { }
}
```

Then use this target with the `PerformanceLoggerBuilder`

```csharp
var consoleTarget = new MyConsoleTarget();

var performanceLogger = new PerformanceLoggerBuilder()
    .AddTarget(consoleTarget)
    .Build();
```

You may extend the builder to add a method specific to your new target as well

```csharp
public static class PerformanceLoggerBuilderExtension
{
    public static PerformanceLoggerBuilder AddConsoleLogger(this PerformanceLoggerBuilder builder)
    {
        var consoleTarget = new MyConsoleTarget();
        return builder.AddTarget(consoleTarget);
    }
}
```

And then use it as follows

```csharp
var performanceLogger = new PerformanceLoggerBuilder()
    .AddConsoleLogger()
    .Build();
```