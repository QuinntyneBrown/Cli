using System.IO;


namespace Cli.Services;
public class SolutionNamespaceProvider : ISolutionNamespaceProvider
{
    private readonly IFileProvider _fileProvider;

    public SolutionNamespaceProvider(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    public string Get(string directory) => Path.GetFileNameWithoutExtension(_fileProvider.Get("*.sln", directory));
}
