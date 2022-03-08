namespace Cli.Services
{
    public interface ICsProjFileManager
    {
        void AddUserSecretsId(string csprojFilePath);
    }
}