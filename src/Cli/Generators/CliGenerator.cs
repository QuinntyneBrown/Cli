using Cli.Strategies;

namespace Cli.Generators
{
    public static class CliGenerator
    {
        public static void Create(CreateCliRequest request, ICliGenerationStrategyFactory factory)
        {
            factory.CreateFor(request);
        }
    }
}
