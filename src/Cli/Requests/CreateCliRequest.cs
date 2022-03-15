using Cli.Models;

namespace Cli
{
    public class CreateCliRequest
    {
        public SolutionModel Model { get; private set; }

        public CreateCliRequest(string name, string directory)
        {
            Model = new SolutionModel(name, directory);
        }
    }
}
