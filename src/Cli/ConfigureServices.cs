using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Cli.Strategies;
using Microsoft.Extensions.Logging;
using System;
using Cli.Services;
using Cli.Factories;
using Cli.Models;
using Cli;

namespace Microsoft.Extensions.Logging;

public static class ConfigureServices
{
    public static void AddCliServices(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddMediatR(typeof(Program));
        services.AddSingleton<ICliGenerationStrategyFactory, CliGenerationStrategyFactory>();
        services.AddSingleton<ICommandService, CommandService>();
        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<ITemplateLocator, TemplateLocator>();
        services.AddSingleton<ITemplateProcessor, LiquidTemplateProcessor>();
        services.AddSingleton<ICsProjFileManager, CsProjFileManager>();
        services.AddSingleton<INamespaceProvider, NamespaceProvider>();
        services.AddSingleton<IFileProvider, FileProvider>();
        services.AddSingleton<ISolutionNamespaceProvider, SolutionNamespaceProvider>();
        services.AddSingleton<IFileFactory, FileFactory>();
        services.AddSingleton<ISolutionFactory, SolutionFactory>();
        services.AddSingleton<IProjectFactory, ProjectFactory>();

        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<Program>(optional: true)
            .Build();

        services.AddSingleton<IConfiguration>(_ => configuration);

    }
}
