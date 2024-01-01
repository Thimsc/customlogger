// See https://aka.ms/new-console-template for more information
using DevOnLogger;
using DevOnLogger.Implementation;
using DevOnLogger.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test;

using IHost host = Host.CreateDefaultBuilder(args).Build();

// Get Configuration section of Logging from appsetting file
ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
IConfiguration c = configurationBuilder.AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
IConfigurationSection logConfig = c.GetSection("Logging");


var services = new ServiceCollection();

services.AddSingleton<Test.LoggerTest>();
services.AddSingleton<Test.TestDemo>();

//Dependency Injenction for custom Logging from configuration file
services.AddTransient(typeof(IDevOnCustomLogger), s =>
{
    var logger = ActivatorUtilities.CreateInstance<DevOnCustomLogger>(s);

    try
    {
        logger.Configure(logConfig);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

    return logger;
});


//Dependency Injenction for custom Logging programatically registering multiple Sink.
    /*services.AddTransient(typeof(IDevOnCustomLogger), s =>
    {
        var logger = ActivatorUtilities.CreateInstance<DevOnCustomLogger>(s);
        logger.RegisterObserver(new DBLogger(logConfig.GetSection("DBProvider").GetSection("ConnectionString").Value));

        return logger;
    });*/

IServiceProvider serviceProvider = services.BuildServiceProvider();

//// TESTING DEVON CUSTOM LOGGING
var service = serviceProvider.GetRequiredService<Test.LoggerTest>();
    await service.Print();
    await service.Print();
    await service.Print();


var service1 = serviceProvider.GetRequiredService<TestDemo>();
await service1.Show();
await service1.Show();
await service1.Show();
await service.Print();
Console.WriteLine("Thank you...");


