using Cli.Models;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;


namespace Cli.Strategies;

public class FileGenerationStrategy
{
    private readonly IFileSystem _fileSystem;
    private readonly ITemplateLocator _templateLocator;
    private readonly ITemplateProcessor _templateProcessor;
    private readonly ILogger _logger;

    public FileGenerationStrategy(
        IFileSystem fileSystem,
        ITemplateLocator templateLocator,
        ITemplateProcessor templateProcessor,
        ILogger logger
        )
    {
        _fileSystem = fileSystem ?? throw new System.ArgumentException(nameof(fileSystem));
        _templateProcessor = templateProcessor ?? throw new System.ArgumentNullException(nameof(templateProcessor));
        _templateLocator = templateLocator ?? throw new System.ArgumentNullException(nameof(templateLocator));
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public void Create(TemplateFileModel model)
    {
        _logger.LogInformation($"Creating {model.Name} file at {model.Path}");

        var template = _templateLocator.Get(model.Template);

        var result = model.ViewModel == null ? template : _templateProcessor.Process(template, model.ViewModel);

        var parts = Path.GetDirectoryName(model.Path).Split(Path.DirectorySeparatorChar);

        for (var i = 1; i <= parts.Length; i++)
        {
            var path = string.Join(Path.DirectorySeparatorChar, parts.Take(i));

            if (!_fileSystem.Exists(path))
            {
                _fileSystem.CreateDirectory(path);
            }
        }

        _fileSystem.WriteAllLines(model.Path, result);
    }
}
