using Cli.Models;
using System.Collections.Generic;


namespace Cli.Factories;

public interface IFileFactory
{
    TemplateFileModel CreateCSharp(string template, string @namespace, string name, string directory, Dictionary<string, object> tokens = null);
    TemplateFileModel CreatePowershell(string template, string name, string directory);
}
