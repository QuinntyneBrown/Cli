using Microsoft.Extensions.Logging;
using System;
using System.IO;

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
            
            _createProjectAndAddToSolution("console", request.Model.SolutionDirectory, request.Model.CliProjectPath, request.Model.CliDirectory);

            _createProjectAndAddToSolution("classlib", request.Model.SolutionDirectory, request.Model.CoreProjectPath, request.Model.CoreDirectory);

            _createProjectAndAddToSolution("classlib", request.Model.SolutionDirectory, request.Model.ApplicationProjectPath, request.Model.ApplicationDirectory);

            _commandService.Start($"dotnet add {request.Model.ApplicationDirectory} reference {request.Model.CoreProjectPath}");

            _commandService.Start($"dotnet add {request.Model.CliDirectory} reference {request.Model.ApplicationProjectPath}");

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
