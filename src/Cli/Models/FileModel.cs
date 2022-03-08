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
        public FileModel(string template, string @namespace, string name, string directory)
        {
            Template = template;
            Namespace = @namespace;
            Name = name;
            Directory = directory;
        }
    }
}
