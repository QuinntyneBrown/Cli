﻿using Cli.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Cli.Strategies
{

    internal class CliGenerationStrategyFactory : ICliGenerationStrategyFactory
    {
        private readonly List<ICliGenerationStrategy> _strategies;

        public CliGenerationStrategyFactory(ICommandService commandService, ILogger logger, IFileSystem fileSystem, ITemplateLocator templateLocator, ITemplateProcessor templateProcessor, ICsProjFileManager csProjFileManager)
        {
            _strategies = new List<ICliGenerationStrategy>()
            {
                new CliGenerationStrategy(commandService,logger,fileSystem, templateLocator, templateProcessor, csProjFileManager)
            };
        }

        public void CreateFor(CreateCliRequest request)
        {
            var strategy = _strategies.Where(x => x.CanHandle(request)).First();

            strategy.Create(request);
        }
    }
}
