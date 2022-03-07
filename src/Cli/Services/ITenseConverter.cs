namespace Cli
{
    public interface ITenseConverter
    {
        string Convert(string value, bool pastTense = true);
    }
}
