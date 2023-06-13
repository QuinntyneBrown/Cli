using Cli.Generators;
using Cli.Models;
using Cli.Strategies;
using CommandLine;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Cli;

internal class Default
{
    [Verb("default")]
    internal class Request : IRequest<Unit>
    {
        [Option('n', "name")]
        public string Name { get; set; }
        [Option('d', Required = false)]
        public string Directory { get; set; } = System.Environment.CurrentDirectory;
    }

    internal class Handler : IRequestHandler<Request, Unit>
    {
        private readonly ICliGenerationStrategyFactory _factory;
        private readonly ISolutionFactory _solutionFactory;
        public Handler(ICliGenerationStrategyFactory factory, ISolutionFactory solutionFactory)
        {
            _factory = factory;
            _solutionFactory = solutionFactory;
        }

        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var model = _solutionFactory.CreateCli(request.Name, request.Directory);

            CliGenerator.Create(new(model), _factory);

            return new();
        }
    }
}
