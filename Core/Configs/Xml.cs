using System;
using System.Collections.Generic;
using System.Xml;
using Node = System.Xml.XmlNode;

namespace FileScraper.Core.Configs
{
    public static class Xml
    {
        public static void LoadConfig(string path)
        {
            XmlDocument config = new XmlDocument();
            config.Load(path);

            foreach (Node node in config.DocumentElement)
            {
                string name = node.Attributes[0].InnerText;

                switch (name)
                {
                    case "Filter":
                        foreach (Node child in node.ChildNodes)
                            AddOption(Options.Filters, child.InnerText);
                        break;
                    case "Links":
                        AddOption(Options.IncludeLinks, bool.Parse(node.ChildNodes[0].InnerText));
                        break;
                        //TODO: more stuff
                }
            }

            Options.UseConfig = true;
        }

        private static void AddOption<T>(List<T> options, T item)
        {
            options.Add(item);
        }
    }
}
