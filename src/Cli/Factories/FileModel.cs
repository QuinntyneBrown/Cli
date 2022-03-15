using Cli.Models;
using System.Collections.Generic;

namespace Cli.Factories
{
    public static class FileFactory
    {
        public static FileModel CreateCSharp(string template, string @namespace, string name, string directory, Dictionary<string,object> tokens = null)
            => new ()
            {
                Extension = "cs",
                Directory = directory,
                Template = template,
                Name = name,
                Tokens = tokens ?? new TokensBuilder()
                .With("Name", (Token)name)
                .With("Namespace", (Token)@namespace)
                .Build()
            };

        public static FileModel CreatePowershell(string template, string name, string directory)
            => new()
            {
                Extension = "ps1",
                Directory = directory,
                Template = template,
                Name=name
            };
    }
}
