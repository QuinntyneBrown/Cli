
namespace Cli.Strategies;

public interface ICliGenerationStrategy
{
    bool CanHandle(CreateCliRequest request);
    void Create(CreateCliRequest request);
}
