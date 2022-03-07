using System.Collections.Generic;

namespace Cli.Models
{
    public class CliProjectModel
    {
        public string Name { get; private set; }
        public string Directory { get; private set; }
        public string Path => $"{Directory}{System.IO.Path.DirectorySeparatorChar}{Name}.csproj";
        public string Namespace => Name;
        public string Type { get; set; }
        public List<CliProjectModel> References { get; set; } = new List<CliProjectModel>();

        public CliProjectModel(string type, string name, string parentDirectory, List<CliProjectModel> references)
            :this(type, name, parentDirectory)
        {
            References = references;
        }

        public CliProjectModel(string type, string name, string parentDirectory)
        {
            Type = type;

            Name = name;

            Directory = $"{parentDirectory}{System.IO.Path.DirectorySeparatorChar}{name}";
        }

        public CliProjectModel()
        {

        }
    }
}
