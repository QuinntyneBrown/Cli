using Cli.Models;

namespace Cli
{
    public class CreateCliRequest
    {
        public CliModel Model { get; private set; }

        public CreateCliRequest(string name, string directory)
        {
            Model = new CliModel(name, directory);
        }
    }
}
