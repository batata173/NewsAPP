using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SantanderN
{
    class DB
    {
        public string GetData(string xmlFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);

            return doc.DocumentElement.SelectSingleNode("max").Value;
        }

        internal void SetDataMaxItem(string xmlFile, int maxItem)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);

            doc.DocumentElement.SelectSingleNode("max").InnerText = maxItem.ToString();
            doc.Save(xmlFile);
        }

        internal void SaveNews(News news)
        {

            XDocument doc = new XDocument(
                                new XDeclaration("1.0", "utf - 16", "true"),
                                new XElement("News",
                                    new XElement("ID", news.id.ToString()),
                                    new XElement("TITLE", news.title),
                                    new XElement("URL", news.url),
                                    new XElement("BY", news.by),
                                    new XElement("TIME", news.time),
                                    new XElement("SCORE", news.score),
                                    new XElement("TCOMMENTS", news.descendants)));

            doc.Save(GenerateXMLFileName(news.id));

        }

        internal bool StoryExists(string id)
        {
            return File.Exists(System.Configuration.ConfigurationManager.AppSettings["StoryDetail"] + id.ToString() + ".xml");
        }

        private string GenerateXMLFileName(int id)
        {
            return System.Configuration.ConfigurationManager.AppSettings["StoryDetail"] + id.ToString() + ".xml";
        }

        internal News StoryFromFile(string id)
        {
            News n = new News();
            XmlDocument doc = new XmlDocument();
            doc.Load(System.Configuration.ConfigurationManager.AppSettings["StoryDetail"] + id.ToString() + ".xml");

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "ID")
                    n.id = Convert.ToInt32(node.InnerText);

                if (node.Name == "TITLE")
                    n.title = node.InnerText;

                if (node.Name == "URL")
                    n.url = node.InnerText;

                if (node.Name == "BY")
                    n.by = node.InnerText;

                if (node.Name == "TIME")
                    n.time = node.InnerText;

                if (node.Name == "SCORE")
                    n.score = Convert.ToInt32(node.InnerText);

                if (node.Name == "TCOMMENTS")
                    n.descendants= Convert.ToInt32(node.InnerText);
            }

            return n;
        }
    }
}
