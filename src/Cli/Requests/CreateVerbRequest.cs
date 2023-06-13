
namespace Cli;

public class CreateVerbRequest
{
    public string Name { get; private set; }
    public string Directory { get; private set; }

    public CreateVerbRequest(string name, string directory)
    {
        Name = name;
        Directory = directory;
    }
}
