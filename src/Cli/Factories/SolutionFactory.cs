using System.Collections.Generic;

namespace Cli.Models
{
    public interface ISolutionFactory
    {
        SolutionModel CreateCli(string name, string directory);
    }

    public class SolutionFactory: ISolutionFactory
    {
        private readonly IProjectFactory _projectFactory;
        public SolutionFactory(IProjectFactory projectFactory)
        {
            _projectFactory = projectFactory;
        }
        public SolutionModel CreateCli(string name, string directory)
        {
            var model = new SolutionModel(name, directory);
            var coreProject = _projectFactory.CreateCore($"{model.Name}.Core", model.SrcDirectory);
            var applicationProject = _projectFactory.CreateApplication($"{model.Name}.Application", model.SrcDirectory, new() { coreProject });
            var cliProject = _projectFactory.CreateCli($"{model.Name}.Cli", model.SrcDirectory, new() { applicationProject });
            model.Projects.AddRange(new List<ProjectModel> { coreProject, applicationProject, cliProject });
            return model;
        }
    }
}
