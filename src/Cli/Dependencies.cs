using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Cli.Strategies;
using Microsoft.Extensions.Logging;
using System;
using Cli.Services;
using Cli.Factories;
using Cli.Models;

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
            services.AddSingleton<ITemplateLocator, TemplateLocator>();
            services.AddSingleton<ITemplateProcessor, LiquidTemplateProcessor>();
            services.AddSingleton(CreateLoggerFactory().CreateLogger("cli"));
            services.AddSingleton<ICsProjFileManager, CsProjFileManager>();
            services.AddSingleton<INamespaceProvider, NamespaceProvider>();
            services.AddSingleton<IFileProvider, FileProvider>();
            services.AddSingleton<ISolutionNamespaceProvider, SolutionNamespaceProvider>();
            services.AddSingleton<IFileFactory, FileFactory>();
            services.AddSingleton<ISolutionFactory, SolutionFactory>();
            services.AddSingleton<IProjectFactory, ProjectFactory>();

            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>(optional:true)
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
