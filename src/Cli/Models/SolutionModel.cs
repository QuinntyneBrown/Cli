using System.Collections.Generic;
using System.IO;

namespace Cli.Models
{
    public class SolutionModel
    {
        public List<ProjectModel> Projects { get; private set; } = new();
        public string Name { get; private set; }
        public string SrcDirectory => $"{SolutionDirectory}{Path.DirectorySeparatorChar}src";
        public string SolutionPath => $"{SolutionDirectory}{Path.DirectorySeparatorChar}{Name}.sln";
        public string Directory { get; private set; }
        public string SolutionDirectory => $"{Directory}{Path.DirectorySeparatorChar}{Name}";
        public SolutionModel(string name, string directory)
        {
            Name = name;
            Directory = directory;
            var coreProject = ProjectModel.CreateCore($"{Name}.Core", SrcDirectory);
            var applicationProject = ProjectModel.CreateApplication($"{Name}.Application", SrcDirectory, new() { coreProject });
            var cliProject = ProjectModel.CreateCli($"{Name}.Cli", SrcDirectory, new() { applicationProject });
            Projects.AddRange(new List<ProjectModel> { coreProject, applicationProject, cliProject });
        }
        public SolutionModel()
        {

        }
    }
}
