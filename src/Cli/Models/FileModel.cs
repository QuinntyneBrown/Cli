using System.Collections.Generic;

namespace Cli.Models
{
    public class FileModel
    {
        public string Template { get; private set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Directory { get; private set; }
        public string Extension { get; private set; } = "cs";
        public string Path => $"{Directory}{System.IO.Path.DirectorySeparatorChar}{Name}.{Extension}";
        public Dictionary<string, object> Tokens { get; private set; } = new();
        public FileModel(string template, string @namespace, string name, string directory, Dictionary<string,object> tokens = null)
        {
            Template = template;
            Namespace = @namespace;
            Name = name;
            Directory = directory;
            Tokens = tokens ?? new TokensBuilder()
                .With(nameof(Name), (Token)Name)
                .With(nameof(Namespace), (Token)Namespace)
                .Build();
        }

        public FileModel(string template, string @namespace, string name, string directory, string extension, Dictionary<string, object> tokens = null)
            :this(template, @namespace, name, directory)
        {
            Extension = extension;
        }
    }
}
