using CommandLine;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cli
{
    internal class Verb
    {
        [Verb("verb")]
        internal class Request : IRequest<Unit> {

        }

        internal class Handler : IRequestHandler<Request, Unit>
        {
            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                return new();
            }
        }
    }
}
