namespace Cli.Services;

public interface ISolutionNamespaceProvider
{
    string Get(string directory);
}
