using System.Collections.Generic;

namespace Cli.Models
{
    public class CliProjectModel
    {
        public string Name { get; private set; }
        public string Directory { get; private set; }
        public string Path => $"{Directory}{System.IO.Path.DirectorySeparatorChar}{Name}.csproj";
        public string Namespace => Name;
        public string Type { get; set; }
        public List<CliProjectModel> References { get; set; } = new List<CliProjectModel>();
        public List<FileModel> Files { get; private set; } = new List<FileModel>();
        public List<PackageModel> Packages { get; private set; } = new();
        public bool HasSecrets { get; set; }

        public CliProjectModel(string type, string name, string parentDirectory, List<CliProjectModel> references)
            :this(type, name, parentDirectory)
        {
            References = references;
        }

        public CliProjectModel(string type, string name, string parentDirectory)
        {
            Type = type;

            Name = name;

            Directory = $"{parentDirectory}{System.IO.Path.DirectorySeparatorChar}{name}";
        }

        public static CliProjectModel CreateCli(string name, string parentDirectory, List<CliProjectModel> references)
        {
            var model = new CliProjectModel("console", name, parentDirectory, references);

            model.HasSecrets = true;

            model.Files.Add(new FileModel("Program", model.Namespace, "Program", model.Directory));

            model.Files.Add(new FileModel("Dependencies", model.Namespace, "Dependencies", model.Directory));


            model.Packages.Add(new("Serilog.Extensions.Hosting", "4.2.0"));

            model.Packages.Add(new("Serilog.Sinks.Console", "2.3.0"));

            model.Packages.Add(new("Serilog.Sinks.Seq", "2.3.0"));

            model.Packages.Add(new("SerilogTimingse", "2.3.0"));

            model.Packages.Add(new("MediatR.Extensions.Microsoft.DependencyInjection", "10.0.1"));

            model.Packages.Add(new("Microsoft.Extensions.Configuration.UserSecrets", "5.0.0"));

            return model;
        }

        public static CliProjectModel CreateCore(string name, string parentDirectory)
        {
            var model = new CliProjectModel("classlib", name, parentDirectory);

            model.Files.Add(new FileModel("Token", model.Namespace, "Token", model.Directory));

            model.Files.Add(new FileModel("NamingConvention", model.Namespace, "NamingConvention", model.Directory));

            model.Files.Add(new FileModel("CommandService", model.Namespace, "CommandService", model.Directory));

            model.Files.Add(new FileModel("ICommandService", model.Namespace, "ICommandService", model.Directory));

            model.Files.Add(new FileModel("FileSystem", model.Namespace, "FileSystem", model.Directory));

            model.Files.Add(new FileModel("IFileSystem", model.Namespace, "IFileSystem", model.Directory));

            model.Files.Add(new FileModel("NamingConventionConverter", model.Namespace, "NamingConventionConverter", model.Directory));

            model.Files.Add(new FileModel("INamingConventionConverter", model.Namespace, "INamingConventionConverter", model.Directory));

            model.Files.Add(new FileModel("TenseConverter", model.Namespace, "TenseConverter", model.Directory));

            model.Files.Add(new FileModel("ITenseConverter", model.Namespace, "ITenseConverter", model.Directory));

            model.Files.Add(new FileModel("TemplateLocator", model.Namespace, "TemplateLocator", model.Directory));

            model.Files.Add(new FileModel("ITemplateLocator", model.Namespace, "ITemplateLocator", model.Directory));

            model.Files.Add(new FileModel("TokensBuilder", model.Namespace, "TokensBuilder", model.Directory));

            model.Files.Add(new FileModel("LiquidTemplateProcessor", model.Namespace, "LiquidTemplateProcessor", model.Directory));

            model.Files.Add(new FileModel("ITemplateProcessor", model.Namespace, "ITemplateProcessor", model.Directory));

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

        public static CliProjectModel CreateApplication(string name, string parentDirectory, List<CliProjectModel> references)
        {
            var model = new CliProjectModel("classlib", name, parentDirectory, references);

            

            return model;
        }

        public CliProjectModel()
        {

        }
    }
}
