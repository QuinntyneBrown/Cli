using Cli.Models;
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
            [Option('n')]
            public string Namespace { get; set; } = "Commands";

            [Option('d', Required = false)]
            public string Directory { get; set; } = System.Environment.CurrentDirectory;
        }

        internal class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IFileSystem _fileSystem;
            private readonly ITemplateLocator _templateLocator;
            private readonly ITemplateProcessor _templateProcessor;
            private readonly ILogger _logger;

            public Handler(
                IFileSystem fileSystem,
                ITemplateLocator templateLocator,
                ITemplateProcessor templateProcessor,
                ILogger logger
                )
            {
                _fileSystem = fileSystem;
                _templateProcessor = templateProcessor;
                _templateLocator = templateLocator;
                _logger = logger;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                FileModel model = new FileModel("Verb", request.Namespace, request.Name, request.Directory);

                new FileGenerationStrategy(_fileSystem,_templateLocator,_templateProcessor,_logger).Create(model);

                return new();
            }
        }
    }
}
