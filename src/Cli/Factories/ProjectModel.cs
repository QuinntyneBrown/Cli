using static Cli.Factories.FileFactory;
using System.Collections.Generic;

namespace Cli.Models
{
    public partial class ProjectModel
    {
        public static ProjectModel CreateCli(string name, string parentDirectory, List<ProjectModel> references)
        {
            var model = new ProjectModel("console", name, parentDirectory, references)
            {
                HasSecrets = true,
                IsNugetPackage = true,
                Order = 1
            };

            model.Files.Add(CreateCSharp("Program", model.Namespace, "Program", model.Directory));

            model.Files.Add(CreateCSharp("Dependencies", model.Namespace, "Dependencies", model.Directory, new TokensBuilder()
                .With(nameof(model.Namespace), (Token)model.Namespace)
                .With("ApplicationNamespace",(Token)model.Name.Replace("Cli","Application"))
                .Build()));

            model.Files.Add(CreatePowershell("Update", "update", model.Directory));

            model.Packages.Add(new("Serilog.Extensions.Hosting", "4.2.0"));

            model.Packages.Add(new("Serilog.Sinks.Console", "2.3.0"));

            model.Packages.Add(new("Serilog.Sinks.Seq", "2.3.0"));

            model.Packages.Add(new("SerilogTimingse", "2.3.0"));

            model.Packages.Add(new("MediatR.Extensions.Microsoft.DependencyInjection", "10.0.1"));

            model.Packages.Add(new("Microsoft.Extensions.Configuration.UserSecrets", "5.0.0"));

            return model;
        }

        public static ProjectModel CreateCore(string name, string parentDirectory)
        {
            var model = new ProjectModel("classlib", name, parentDirectory);

            model.Files.Add(CreateCSharp("Token", model.Namespace, "Token", model.Directory));
            
            model.Files.Add(CreateCSharp("NamingConvention", model.Namespace, "NamingConvention", model.Directory));
            
            model.Files.Add(CreateCSharp("CommandService", model.Namespace, "CommandService", model.Directory));
            
            model.Files.Add(CreateCSharp("ICommandService", model.Namespace, "ICommandService", model.Directory));
            
            model.Files.Add(CreateCSharp("FileSystem", model.Namespace, "FileSystem", model.Directory));
            
            model.Files.Add(CreateCSharp("IFileSystem", model.Namespace, "IFileSystem", model.Directory));
            
            model.Files.Add(CreateCSharp("NamingConventionConverter", model.Namespace, "NamingConventionConverter", model.Directory));
            
            model.Files.Add(CreateCSharp("INamingConventionConverter", model.Namespace, "INamingConventionConverter", model.Directory));
            
            model.Files.Add(CreateCSharp("TenseConverter", model.Namespace, "TenseConverter", model.Directory));
            
            model.Files.Add(CreateCSharp("ITenseConverter", model.Namespace, "ITenseConverter", model.Directory));
            
            model.Files.Add(CreateCSharp("TemplateLocator", model.Namespace, "TemplateLocator", model.Directory));
            
            model.Files.Add(CreateCSharp("ITemplateLocator", model.Namespace, "ITemplateLocator", model.Directory));
            
            model.Files.Add(CreateCSharp("TokensBuilder", model.Namespace, "TokensBuilder", model.Directory));
            
            model.Files.Add(CreateCSharp("LiquidTemplateProcessor", model.Namespace, "LiquidTemplateProcessor", model.Directory));
            
            model.Files.Add(CreateCSharp("ITemplateProcessor", model.Namespace, "ITemplateProcessor", model.Directory));

            model.Files.Add(CreateCSharp("NamespaceProvider", model.Namespace, "NamespaceProvider", model.Directory));

            model.Files.Add(CreateCSharp("INamespaceProvider", model.Namespace, "INamespaceProvider", model.Directory));

            model.Files.Add(CreateCSharp("FileProvider", model.Namespace, "FileProvider", model.Directory));

            model.Files.Add(CreateCSharp("IFileProvider", model.Namespace, "IFileProvider", model.Directory));

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

        public static ProjectModel CreateApplication(string name, string parentDirectory, List<ProjectModel> references)
        {
            var model = new ProjectModel("classlib", name, parentDirectory, references);

            model.Files.Add(CreateCSharp("Default", model.Namespace, "Default", model.Directory));

            return model;
        }

    }
}
