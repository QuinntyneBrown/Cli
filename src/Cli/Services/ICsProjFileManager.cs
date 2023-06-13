using Cli.Models;


namespace Cli.Services;

public interface ICsProjFileManager
{
    void AddUserSecretsId(string csprojFilePath);
    void AddNugetConfiguration(ProjectModel model);
}
