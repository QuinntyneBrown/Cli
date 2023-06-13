namespace Cli;

public interface ITemplateProcessor
{
    string[] Process(string[] template, dynamic model);
    string Process(string template, dynamic model);
}
