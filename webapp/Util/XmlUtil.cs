using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;

namespace webapp.Util
{

    public static class XmlUtil
    {
        public static XPathNodeIterator GetDocumentNodes(
            string filepath,
            string xpath
            )
        {
            //XmlDocument document = new XmlDocument();
            //document.Load(filepath);

            XPathDocument document = new XPathDocument(filepath);
            XPathNavigator navigator = document.CreateNavigator();
            XPathNodeIterator nodes = navigator.Select(xpath);

            return nodes;
        }


        // @param XPathNodeIterator nodes : nodes to convert
        public static string NodesToUL(
            XPathNodeIterator nodes
            )
        {
            if (nodes.Count == 0)
            {
                return "<ul><li>EMPTY</li></ul>";
            }

           // XmlReader xml = navigator.ReadSubtree();

            var result = "<ul>";
            while (nodes.MoveNext())
            {
                result += "<li>" + nodes.Current.Name + " : " + nodes.Current.Value;
                if (nodes.Current.HasAttributes && nodes.Current.MoveToFirstAttribute())
                {
                    result += "<ul>";
                    result += "<li>@" + nodes.Current.Name + " : " + nodes.Current.Value + "</li>";
                    while (nodes.Current.MoveToNextAttribute())
                    { result += "<li>@" + nodes.Current.Name + " : " + nodes.Current.Value + "</li>"; }
                    result += "</ul>";

                    nodes.Current.MoveToParent();
                }
                // subnodes
                if (nodes.Current.HasChildren && nodes.Current.MoveToFirstChild())
                {
                    result += "<ul>";
                    result += "<li>sub " + nodes.Current.Name + " : " + nodes.Current.Value + "</li>";
                    while (nodes.Current.MoveToNext(XPathNodeType.Element))
                    { result += "<li>sub " + nodes.Current.Name + " : " + nodes.Current.Value + "</li>"; }
                    result += "</ul>";

                    nodes.Current.MoveToParent();
                }

                result += "</li>";
            }
            result += "</ul>";

            return result;
        }



    }

}
