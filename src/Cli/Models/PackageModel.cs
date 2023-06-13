
namespace Cli.Models;

public class PackageModel
{
    public string Name { get; init; }
    public string Version { get; init; }

    public PackageModel(string name)
    {
        Name = name;
    }

    public PackageModel(string name, string verison)
        : this(name)
    {
        Version = verison;
    }

    public PackageModel()
    {

    }
}
