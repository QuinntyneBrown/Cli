using Cli.Models;
using Cli.Services;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Cli.Strategies
{
    public class ProjectGenerationStrategy
    {
        private readonly FileGenerationStrategy _fileGenerationStrategy;
        private readonly ICommandService _commandService;
        private readonly ICsProjFileManager _csProjFileManager;

        public ProjectGenerationStrategy(
            IFileSystem fileSystem,
            ITemplateLocator templateLocator,
            ITemplateProcessor templateProcessor,
            ILogger logger,
            ICommandService commandService,
            ICsProjFileManager csProjFileManager,
            ISolutionNamespaceProvider solutionNamespaceProvider
            )
        {
            _commandService = commandService;
            _fileGenerationStrategy = new(fileSystem, templateLocator, templateProcessor, solutionNamespaceProvider, logger);
            _csProjFileManager = csProjFileManager;
        }

        public void Create(ProjectModel model)
        {
            foreach(var path in Directory.GetFiles(model.Directory,"*.cs",SearchOption.AllDirectories))
            {
                System.IO.File.Delete(path);
            }

            foreach(var package in model.Packages)
            {
                _commandService.Start($"dotnet add package {package.Name} --version {package.Version}",model.Directory);
            }

            foreach(var file in model.Files)
            {
                _fileGenerationStrategy.Create(file);
            }

            if(model.HasSecrets)
            {
                _csProjFileManager.AddUserSecretsId(model.Path);
            }

            if(model.IsNugetPackage)
            {
                _csProjFileManager.AddNugetConfiguration(model);
            }
        }
    }
}
