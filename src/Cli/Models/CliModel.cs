using System.Collections.Generic;
using System.IO;

namespace Cli.Models
{
    public class CliModel
    {
        public List<CliProjectModel> Projects { get; private set; } = new();
        public string Name { get; private set; }
        public string CliNamespace => $"{Name}.Cli";
        public string ApplicationNamespace => $"{Name}.Application";
        public string CoreNamespace => $"{Name}.Core";
        public string SrcDirectory => $"{SolutionDirectory}{Path.DirectorySeparatorChar}src";
        public string ApplicationDirectory => $"{SrcDirectory}{Path.DirectorySeparatorChar}{Name}.Application";
        public string CoreDirectory => $"{SrcDirectory}{Path.DirectorySeparatorChar}{Name}.Core";
        public string CliDirectory => $"{SrcDirectory}{Path.DirectorySeparatorChar}{Name}.Cli";
        public string ApplicationProjectPath => $"{ApplicationDirectory}{Path.DirectorySeparatorChar}{Name}.Application.csproj";
        public string CoreProjectPath => $"{CoreDirectory}{Path.DirectorySeparatorChar}{Name}.Core.csproj";
        public string CliProjectPath => $"{CliDirectory}{Path.DirectorySeparatorChar}{Name}.Cli.csproj";
        public string SolutionPath => $"{SolutionDirectory}{Path.DirectorySeparatorChar}{Name}.sln";
        public string Directory { get; private set; }
        public string SolutionDirectory => $"{Directory}{Path.DirectorySeparatorChar}{Name}";
        public CliModel(string name, string directory)
        {
            Name = name;
            Directory = directory;

            Projects.Add(new($"{Name}.Core", SrcDirectory));
            Projects.Add(new($"{Name}.Application", SrcDirectory));
            Projects.Add(new($"{Name}.Cli", SrcDirectory));

        }
        public CliModel()
        {

        }
    }
}
