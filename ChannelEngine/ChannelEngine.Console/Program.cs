﻿using ChannelEngine.Services;
using ChannelEngine.ViewModels.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace ChannelEngine.Console
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // create service collection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            var serviceProvider = services.BuildServiceProvider();

            // entry to run app
            await serviceProvider.GetService<App>().Run(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // configure logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            // build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            services.Configure<ChannelEngineOptions>(configuration.GetSection(ChannelEngineOptions.ChannelEngine));

            // Services
            services.AddSingleton<IChannelEngineService, ChannelEngineService>();
            services.AddScoped<IProductService, ProductService>();

            // add app
            services.AddTransient<App>();
        }
    }
}
