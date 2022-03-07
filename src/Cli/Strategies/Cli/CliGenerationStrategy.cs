using Microsoft.Extensions.Logging;
using System;

namespace Cli.Strategies
{
    internal class CliGenerationStrategy : ICliGenerationStrategy
    {
        private readonly ICommandService _commandService;
        private readonly IFileSystem _fileSystem;
        private readonly ILogger _logger;

        public CliGenerationStrategy(ICommandService commandService, ILogger logger, IFileSystem fileSystem)
        {
            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService)); 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public bool CanHandle(CreateCliRequest request) => true;

        public void Create(CreateCliRequest request)
        {
            _fileSystem.CreateDirectory(request.Model.SolutionDirectory);

            _commandService.Start($"dotnet new sln -n {request.Model.Name}", request.Model.SolutionDirectory);

            _fileSystem.CreateDirectory(request.Model.SrcDirectory);

            foreach (var project in request.Model.Projects)
            {
                _createProjectAndAddToSolution(project.Type, request.Model.SolutionDirectory, project.Path, project.Directory);
            }

            foreach (var project in request.Model.Projects)
            {
                foreach(var reference in project.References)
                {
                    _commandService.Start($"dotnet add {project.Directory} reference {reference.Path}");
                }
            }

            _commandService.Start("code .", request.Model.SolutionDirectory);
        }

        private void _createProjectAndAddToSolution(string templateType, string directory, string projectPath, string projectDirectory)
        {
            _fileSystem.CreateDirectory(projectDirectory);

            _commandService.Start($"dotnet new {templateType} --framework net6.0", projectDirectory);

            _commandService.Start($"dotnet sln add {projectPath}", directory);
        }
    }
}
