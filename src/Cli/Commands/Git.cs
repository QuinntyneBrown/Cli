using CommandLine;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Octokit;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;


namespace Cli.Commands;

internal class Git
{
    [Verb("git")]
    internal class Request : IRequest<Unit>
    {
        [Value(0)]
        public string RepositoryName { get; set; }

        [Option('d', Required = false)]
        public string Directory { get; set; } = Environment.CurrentDirectory;
    }

    internal class Handler : IRequestHandler<Request, Unit>
    {
        private readonly ICommandService _commandService;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ITemplateLocator _templateLocator;
        private readonly IFileSystem _fileSystem;

        public Handler(ILogger logger, IConfiguration configuration, ICommandService commandService, ITemplateLocator templateLocator, IFileSystem fileSystem)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            _templateLocator = templateLocator ?? throw new ArgumentNullException(nameof(templateLocator));
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handled: {nameof(Git)}");


            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            KeyValueConfigurationCollection settings = config.AppSettings.Settings;

            settings.Add(new KeyValueConfigurationElement("GitUsername", "QuinntyneBrown"));

            config.Save(ConfigurationSaveMode.Modified);

            return await Task.FromResult(new Unit());

            var client = new GitHubClient(new ProductHeaderValue(_configuration["GitHubUsername"]))
            {
                Credentials = new Credentials(_configuration["GitHubPersonalAccessToken"])
            };

            client.Repository.Create(new NewRepository(request.RepositoryName)).GetAwaiter().GetResult();

            _commandService.Start($"git init", $@"{request.Directory}");

            _commandService.Start($"git config user.name {_configuration["GitHubUsername"]}", request.Directory);

            _commandService.Start($"git config user.email {_configuration["GitHubEmail"]}", request.Directory);

            _fileSystem.WriteAllLines($@"{request.Directory}\.gitignore", _templateLocator.Get("GitIgnore"));

            _commandService.Start($"git remote add origin https://{_configuration["GitHubUsername"]}:{_configuration["GitHubPersonalAccessToken"]}@github.com/{_configuration["GitHubUsername"]}/{request.RepositoryName}.git");

            return new();
        }
    }
}
