namespace Cli.Strategies
{
    public interface ICliGenerationStrategyFactory
    {
        void CreateFor(CreateCliRequest request);
    }
}
