using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Cli.Strategies;
using Microsoft.Extensions.Logging;
using System;

namespace Cli
{
    public class Dependencies
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddMediatR(typeof(Program));
            services.AddSingleton<ICliGenerationStrategyFactory, CliGenerationStrategyFactory>();
            services.AddSingleton<ICommandService, CommandService>();
            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddSingleton(CreateLoggerFactory().CreateLogger("cli"));

            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            services.AddSingleton<IConfiguration>(_ => configuration);
            
        }

        private static ILoggerFactory CreateLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddProvider(new LoggerProvider(new LoggerOptions(true, ConsoleColor.Red, ConsoleColor.DarkYellow, Console.Out)));
            });
        }
    }
}
