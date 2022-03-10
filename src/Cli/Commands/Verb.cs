using Cli.Models;
using Cli.Services;
using Cli.Strategies;
using CommandLine;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Cli
{
    internal class Verb
    {
        [Verb("verb")]
        internal class Request : IRequest<Unit> {
            [Value(0)]
            public string Name { get; set; }

            [Option('n', Required = false)]
            public string Namespace { get; set; }

            [Option('d', Required = false)]
            public string Directory { get; set; } = System.Environment.CurrentDirectory;
        }

        internal class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IFileSystem _fileSystem;
            private readonly ITemplateLocator _templateLocator;
            private readonly ITemplateProcessor _templateProcessor;
            private readonly INamespaceProvider _namespaceProvider;
            private readonly ILogger _logger;

            public Handler(
                IFileSystem fileSystem,
                ITemplateLocator templateLocator,
                ITemplateProcessor templateProcessor,
                ILogger logger,
                INamespaceProvider namespaceProvider
                )
            {
                _fileSystem = fileSystem;
                _templateProcessor = templateProcessor;
                _templateLocator = templateLocator;
                _logger = logger;
                _namespaceProvider = namespaceProvider;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                FileModel model = new FileModel("Verb", request.Namespace ?? _namespaceProvider.Get(request.Directory), request.Name, request.Directory);

                new FileGenerationStrategy(_fileSystem,_templateLocator,_templateProcessor,_logger).Create(model);

                return new();
            }
        }
    }
}
