using System.Collections.Generic;


namespace Cli.Models;

public class SolutionFactory : ISolutionFactory
{
    private readonly IProjectFactory _projectFactory;
    public SolutionFactory(IProjectFactory projectFactory)
    {
        _projectFactory = projectFactory;
    }
    public SolutionModel CreateCli(string name, string directory)
    {
        var model = new SolutionModel(name, directory);
        var applicationProject = _projectFactory.CreateApplication($"{model.Name}", model.SrcDirectory);
        var cliProject = _projectFactory.CreateCli($"{model.Name}.Cli", model.SrcDirectory, new() { applicationProject });
        model.Projects.AddRange(new List<ProjectModel> { applicationProject, cliProject });
        return model;
    }
}
