using System.Collections.Generic;

namespace Cli.Models
{
    public partial class SolutionModel
    {
        public static SolutionModel CreateCli(string name, string directory)
        {
            var model = new SolutionModel(name, directory);
            var coreProject = ProjectModel.CreateCore($"{model.Name}.Core", model.SrcDirectory);
            var applicationProject = ProjectModel.CreateApplication($"{model.Name}.Application", model.SrcDirectory, new() { coreProject });
            var cliProject = ProjectModel.CreateCli($"{model.Name}.Cli", model.SrcDirectory, new() { applicationProject });
            model.Projects.AddRange(new List<ProjectModel> { coreProject, applicationProject, cliProject });
            return model;
        }
    }
}
