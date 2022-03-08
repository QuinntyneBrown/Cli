using Cli.Models;
using Microsoft.Extensions.Logging;

namespace Cli.Strategies
{
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

        public void Create(FileModel model)
        {
            _logger.LogInformation($"Creating {model.Name} file at {model.Path}");

            var template = _templateLocator.Get(model.Template);

            var tokens = new TokensBuilder()
                .With(nameof(model.Name), (Token)model.Name)
                .With(nameof(model.Namespace), (Token)model.Namespace)
                .Build();

            var result = _templateProcessor.Process(template, tokens);

            _fileSystem.WriteAllLines(model.Path, result);
        }
    }
}
