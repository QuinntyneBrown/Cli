using Cli.Models;
using Cli.Services;
using System.Collections.Generic;

namespace Cli.Factories
{

    public class FileFactory: IFileFactory
    {
        private readonly ISolutionNamespaceProvider _solutionNamespaceProvider;

        public FileFactory(ISolutionNamespaceProvider solutionNamespaceProvider)
        {
            _solutionNamespaceProvider = solutionNamespaceProvider;
        }

        public FileModel CreateCSharp(string template, string @namespace, string name, string directory, Dictionary<string, object> tokens = null)
        {
            if(tokens != null)
            {
                foreach(var token in new TokensBuilder().With("SolutionNamespace", (Token)_solutionNamespaceProvider.Get(directory)).Build())
                {
                    tokens.Add(token.Key, token.Value);
                }
            }

            return new()
            {
                Extension = "cs",
                Directory = directory,
                Template = template,
                Name = name,
                Tokens = tokens ?? new TokensBuilder()
                .With("Name", (Token)name)
                .With("Namespace", (Token)@namespace)
                .With("SolutionNamespace", (Token)_solutionNamespaceProvider.Get(directory))
                .Build()

            };
        }


        public FileModel CreatePowershell(string template, string name, string directory)
            => new()
            {
                Extension = "ps1",
                Directory = directory,
                Template = template,
                Name=name
            };
    }
}
