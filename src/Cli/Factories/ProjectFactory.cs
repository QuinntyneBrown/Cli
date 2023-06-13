using Cli.Factories;
using System.Collections.Generic;
using System.IO;


namespace Cli.Models;

public class ProjectFactory : IProjectFactory
{
    private readonly IFileFactory _fileFactory;

    public ProjectFactory(IFileFactory fileFactory)
    {
        _fileFactory = fileFactory;
    }

    private TemplateFileModel _createCSharp(string template, string @namespace, string name, string directory, Dictionary<string, object> tokens = null)
    {
        return _fileFactory.CreateCSharp(template, @namespace, name, directory, tokens);
    }

    private TemplateFileModel _createPowershell(string template, string name, string directory)
    {
        return _fileFactory.CreatePowershell(template, name, directory);
    }
    public ProjectModel CreateCli(string name, string parentDirectory, List<ProjectModel> references)
    {
        var model = new ProjectModel("console", name, parentDirectory, references)
        {
            HasSecrets = true,
            IsNugetPackage = true,
            Order = 1
        };

        model.Files.Add(_createCSharp("Program", model.Namespace, "Program", model.Directory));

        model.Files.Add(_createCSharp("ConsoleLogger", model.Namespace, "ConsoleLogger", $"{model.Directory}{Path.DirectorySeparatorChar}Logging"));

        model.Files.Add(_createCSharp("LoggerOptions", model.Namespace, "LoggerOptions", $"{model.Directory}{Path.DirectorySeparatorChar}Logging"));

        model.Files.Add(_createCSharp("LoggerProvider", model.Namespace, "LoggerProvider", $"{model.Directory}{Path.DirectorySeparatorChar}Logging"));

        model.Files.Add(_createCSharp("Dependencies", model.Namespace, "Dependencies", model.Directory, new TokensBuilder()
            .With(nameof(model.Namespace), (Token)model.Namespace)
            .With("ApplicationNamespace", (Token)model.Name.Replace("Cli", "Application"))
            .With("CoreNamespace", (Token)model.Name.Replace("Cli", "Core"))
            .Build()));

        model.Files.Add(_createPowershell("Update", "update", model.Directory));

        model.Packages.Add(new("Serilog.Extensions.Hosting", "4.2.0"));

        model.Packages.Add(new("Serilog.Sinks.Console", "2.3.0"));

        model.Packages.Add(new("Serilog.Sinks.Seq", "2.3.0"));

        model.Packages.Add(new("SerilogTimingse", "2.3.0"));

        model.Packages.Add(new("MediatR.Extensions.Microsoft.DependencyInjection", "10.0.1"));

        model.Packages.Add(new("Microsoft.Extensions.Configuration.UserSecrets", "5.0.0"));

        return model;
    }

    public ProjectModel CreateApplication(string name, string parentDirectory)
    {
        var model = new ProjectModel("classlib", name, parentDirectory);

        model.Files.Add(_createCSharp("Default", model.Namespace, "Default", $"{model.Directory}{Path.DirectorySeparatorChar}Commands"));

        model.Files.Add(_createCSharp("Token", model.Namespace, "Token", $"{model.Directory}{Path.DirectorySeparatorChar}ValueObjects"));

        model.Files.Add(_createCSharp("FileModel", model.Namespace, "FileModel", $"{model.Directory}{Path.DirectorySeparatorChar}Models"));

        model.Files.Add(_createCSharp("TemplateFileModel", model.Namespace, "TemplateFileModel", $"{model.Directory}{Path.DirectorySeparatorChar}Models"));

        model.Files.Add(_createCSharp("FileFactory", model.Namespace, "FileFactory", $"{model.Directory}{Path.DirectorySeparatorChar}Factories"));

        model.Files.Add(_createCSharp("IFileFactory", model.Namespace, "IFileFactory", $"{model.Directory}{Path.DirectorySeparatorChar}Factories"));

        model.Files.Add(_createCSharp("FileGenerationStrategy", model.Namespace, "FileGenerationStrategy", $"{model.Directory}{Path.DirectorySeparatorChar}Strategies"));

        model.Files.Add(_createCSharp("IFileGenerationStrategy", model.Namespace, "IFileGenerationStrategy", $"{model.Directory}{Path.DirectorySeparatorChar}Strategies"));

        model.Files.Add(_createCSharp("NamingConvention", model.Namespace, "NamingConvention", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("CommandService", model.Namespace, "CommandService", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("ICommandService", model.Namespace, "ICommandService", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("FileSystem", model.Namespace, "FileSystem", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("IFileSystem", model.Namespace, "IFileSystem", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("NamingConventionConverter", model.Namespace, "NamingConventionConverter", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("INamingConventionConverter", model.Namespace, "INamingConventionConverter", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("TenseConverter", model.Namespace, "TenseConverter", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("ITenseConverter", model.Namespace, "ITenseConverter", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("TemplateLocator", model.Namespace, "TemplateLocator", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("ITemplateLocator", model.Namespace, "ITemplateLocator", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("TokensBuilder", model.Namespace, "TokensBuilder", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("LiquidTemplateProcessor", model.Namespace, "LiquidTemplateProcessor", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("ITemplateProcessor", model.Namespace, "ITemplateProcessor", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("NamespaceProvider", model.Namespace, "NamespaceProvider", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("INamespaceProvider", model.Namespace, "INamespaceProvider", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("FileProvider", model.Namespace, "FileProvider", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Files.Add(_createCSharp("IFileProvider", model.Namespace, "IFileProvider", $"{model.Directory}{Path.DirectorySeparatorChar}Services"));

        model.Packages.Add(new("Microsoft.Extensions.Configuration", "6.0.0"));

        model.Packages.Add(new("CSharpFunctionalExtensions", "2.15.0"));

        model.Packages.Add(new("DotLiquid", "2.0.395"));

        model.Packages.Add(new("Humanizer.Core", "2.14.1"));

        model.Packages.Add(new("MediatR", "10.0.1"));

        model.Packages.Add(new("Microsoft.CSharp", "4.7.0"));

        model.Packages.Add(new("Microsoft.EntityFrameworkCore.Design", "6.0.0"));

        model.Packages.Add(new("Microsoft.Extensions.DependencyInjection", "6.0.0"));

        model.Packages.Add(new("Microsoft.Extensions.DependencyInjection.Abstractions", "6.0.0"));

        model.Packages.Add(new("Microsoft.Extensions.Logging", "6.0.0"));

        model.Packages.Add(new("Newtonsoft.Json", "12.0.3"));

        model.Packages.Add(new("System.Collections", "4.3.0"));

        model.Packages.Add(new("CommandLineParser", "2.8.0"));

        model.Packages.Add(new("SharpSimpleNLG", "1.2.1"));

        model.Packages.Add(new("System.Reactive", "5.0.0"));

        return model;
    }
}
