using System.Collections.Generic;
using System.IO;

namespace Cli.Models
{
    public class CliModel
    {
        public List<CliProjectModel> Projects { get; private set; } = new();
        public string Name { get; private set; }
        public string SrcDirectory => $"{SolutionDirectory}{Path.DirectorySeparatorChar}src";
        public string SolutionPath => $"{SolutionDirectory}{Path.DirectorySeparatorChar}{Name}.sln";
        public string Directory { get; private set; }
        public string SolutionDirectory => $"{Directory}{Path.DirectorySeparatorChar}{Name}";
        public CliModel(string name, string directory)
        {
            Name = name;
            Directory = directory;
            var coreProject = CliProjectModel.CreateCore($"{Name}.Core", SrcDirectory);
            var applicationProject = CliProjectModel.CreateApplication($"{Name}.Application", SrcDirectory, new() { coreProject });
            var cliProject = CliProjectModel.CreateCli($"{Name}.Cli", SrcDirectory, new() { applicationProject });
            Projects.AddRange(new List<CliProjectModel> { coreProject, applicationProject, cliProject });
        }
        public CliModel()
        {

        }
    }
}
