using Cli.Generators;
using Cli.Strategies;
using CommandLine;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cli
{
    internal class Default
    {
        [Verb("default")]
        internal class Request : IRequest<Unit> {
            [Option('n',"name")]
            public string Name { get; set; }
            [Option('d', Required = false)]
            public string Directory { get; set; } = System.Environment.CurrentDirectory;
        }

        internal class Handler : IRequestHandler<Request, Unit>
        {
            private readonly ICliGenerationStrategyFactory _factory;
            public Handler(ICliGenerationStrategyFactory factory)
            {
                _factory = factory;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                CliGenerator.Create(new (request.Name, request.Directory), _factory);

                return new();
            }
        }
    }
}
