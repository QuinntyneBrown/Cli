namespace Cli.Models;

public interface ISolutionFactory
{
    SolutionModel CreateCli(string name, string directory);
}
