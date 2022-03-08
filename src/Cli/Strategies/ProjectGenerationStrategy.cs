using Cli.Models;
using Microsoft.Extensions.Logging;

namespace Cli.Strategies
{
    public class ProjectGenerationStrategy
    {
        private readonly FileGenerationStrategy _fileGenerationStrategy;
        public ProjectGenerationStrategy(
            IFileSystem fileSystem,
            ITemplateLocator templateLocator,
            ITemplateProcessor templateProcessor,
            ILogger logger
            )
        {
            _fileGenerationStrategy = new(fileSystem, templateLocator, templateProcessor, logger);
        }

        public void Create(CliProjectModel model)
        {
            foreach(var file in model.Files)
            {
                _fileGenerationStrategy.Create(file);
            }
        }
    }
}
