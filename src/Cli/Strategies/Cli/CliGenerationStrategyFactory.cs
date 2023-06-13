using Cli.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;


namespace Cli.Strategies;


internal class CliGenerationStrategyFactory : ICliGenerationStrategyFactory
{
    private readonly List<ICliGenerationStrategy> _strategies;

    public CliGenerationStrategyFactory(ICommandService commandService, ILogger<CliGenerationStrategy> logger, IFileSystem fileSystem, ITemplateLocator templateLocator, ITemplateProcessor templateProcessor, ICsProjFileManager csProjFileManager, ISolutionNamespaceProvider solutionNamespaceProvider)
    {
        _strategies = new List<ICliGenerationStrategy>()
        {
            new CliGenerationStrategy(commandService,logger,fileSystem, templateLocator, templateProcessor, csProjFileManager, solutionNamespaceProvider)
        };
    }

    public void CreateFor(CreateCliRequest request)
    {
        var strategy = _strategies.Where(x => x.CanHandle(request)).First();

        strategy.Create(request);
    }
}
