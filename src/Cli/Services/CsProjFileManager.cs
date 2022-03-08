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
    }
}