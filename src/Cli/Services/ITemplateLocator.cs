namespace Cli
{
    public interface ITemplateLocator
    {
        string[] Get(string filename);
    }
}