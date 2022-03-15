using Cli.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Cli.Services
{
    public class CsProjFileManager : ICsProjFileManager
    {
        public void AddUserSecretsId(string csprojFilePath)
        {
            var doc = XDocument.Load(csprojFilePath);
            var projectNode = doc.FirstNode as XElement;

            var element = projectNode.Nodes()
                .Where(x => x.NodeType == System.Xml.XmlNodeType.Element)
                .First(x => (x as XElement).Name == "PropertyGroup") as XElement;

            element.Add(new XElement("UserSecretsId", $"{Guid.NewGuid()}"));
            doc.Save(csprojFilePath);
        }

        public void AddNugetConfiguration(ProjectModel model)
        {
            var doc = XDocument.Load(model.Path);
            var projectNode = doc.FirstNode as XElement;

            var element = projectNode.Nodes()
                .Where(x => x.NodeType == System.Xml.XmlNodeType.Element)
                .First(x => (x as XElement).Name == "PropertyGroup") as XElement;

            element.Add(new XElement("PackAsTool", true));

            element.Add(new XElement("ToolCommandName", model.Name.Split('.')[0].ToLower()));

            element.Add(new XElement("Version", "1.0.0"));

            element.Add(new XElement("PackageOutputPath", "./nupkg"));

            doc.Save(model.Path);
        }
    }
}