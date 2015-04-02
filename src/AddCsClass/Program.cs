using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AddCsClass
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Count() != 2)
            {
                HelpMenu();
                return;
            }


            var projectFile = args[0];
            var newSourceFile = args[1];


            AddFileProject(newSourceFile, projectFile);

        }

        private static void HelpMenu()
        {
            System.Console.WriteLine("This program will add a .cs file to an existing c# project.");
            System.Console.WriteLine("");
            System.Console.WriteLine("AddCsClass.exe \"path to project file.csproj\" \"path to new .cs file\"");

        }

        private static XElement AddProjectContent(string pathToAdd, XDocument doc)
        {
            XNamespace rootNamespace = doc.Root.Name.NamespaceName;
            var xelem = new XElement(rootNamespace + "Compile");
            xelem.Add(new XAttribute("Include", pathToAdd));
            return xelem;
        }

        private static void AddFileProject(string pathToAdd, string projectFilePath)
        {

            var projectFileXml = XDocument.Load(projectFilePath);
            var itemGroup =
            projectFileXml.Nodes()
                               .OfType<XElement>()
                               .DescendantNodes()
                               .OfType<XElement>().First(xy => xy.Name.LocalName == "ItemGroup");
            var xelem = AddProjectContent(pathToAdd, projectFileXml);
            itemGroup.Add(xelem);
            projectFileXml.Save(projectFilePath);
        }

    }
}
