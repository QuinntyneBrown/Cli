using System.Collections.Generic;

namespace Cli.Models
{
    public partial class FileModel
    {
        public static FileModel CreateCSharp(string template, string @namespace, string name, string directory, Dictionary<string,object> tokens = null)
        {
            var model = new FileModel(template, @namespace, name, directory, tokens);
            model.Extension = "cs";
            model.Tokens = tokens ?? new TokensBuilder()
                .With(nameof(Name), (Token)model.Name)
                .With(nameof(Namespace), (Token)model.Namespace)
                .Build();

            return model;
        }
    }
}
