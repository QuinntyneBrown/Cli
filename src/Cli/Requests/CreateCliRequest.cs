using Cli.Models;

namespace Cli
{
    public class CreateCliRequest
    {
        public SolutionModel Model { get; init; }

        public CreateCliRequest(SolutionModel model)
        {
            Model = model;
        }
    }
}
