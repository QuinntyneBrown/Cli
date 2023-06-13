using System.Collections.Generic;


namespace Cli.Models;

public interface IProjectFactory
{
    ProjectModel CreateCli(string name, string parentDirectory, List<ProjectModel> references);
    ProjectModel CreateApplication(string name, string parentDirectory);
}
