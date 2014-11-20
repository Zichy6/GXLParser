using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GXLParser
{
    class Program
    {
        static GXL gxl = new GXL();

        static void Main(string[] args)
        {
            LoadGraph();
            //gxl.GetAllGraphs();
            //ReadXML();
            Console.Read();
        }

        private static void ParseAttr(XmlReader reader)
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                Attr attr = new Attr(reader.GetAttribute("name"), reader.GetAttribute("id"), reader.GetAttribute("kind"));
                List<Attr> attrs = new List<Attr>();
                attrs.Add(attr);
                foreach (Attr attr1 in attrs)
                {
                    Console.WriteLine("Attr name: " + attr);
                }

                XmlReader attrReader = reader.ReadSubtree();
                while (attrReader.Read())
                {
                    switch (attrReader.Name)
                    {
                        case "int":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsInt());
                            }
                            break;
                        case "string":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsString());
                            }
                            break;
                        case "bool":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsBoolean());
                            }
                            break;
                        case "float":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsFloat());
                            }
                            break;
                        case "enum":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsString());
                            }
                            break;
                        case "seq":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                //ParseAttr(reader);
                            }
                            break;
                        case "set":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                //ParseAttr(reader);
                            }
                            break;
                        case "bag":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                //ParseAttr(reader);
                            }
                            break;
                        case "tup":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                //ParseAttr(reader);
                            }
                            break;
                    }
                }
            }
        }

        private static void LoadGraph()
        {
            XmlReaderSettings set = new XmlReaderSettings();
            set.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(@"C:\Users\Crossfire\Documents\Visual Studio 2012\Projects\GXLParser\GXLParser\GXL.xml", set);
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "graph":
                            //reader.ReadElementContentAsBoolean("edgeids", reader.NamespaceURI) pro cteni v elementu
                            Graph g = new Graph(reader.GetAttribute("id"), Convert.ToBoolean(reader.GetAttribute("edgeids")), reader.GetAttribute("edgemode"), Convert.ToBoolean(reader.GetAttribute("hypergraph")));
                            //gxl.AddGraph(g);
                            Console.WriteLine(g);
                            XmlReader subreader = reader.ReadSubtree();
                            while (subreader.Read())
                            {
                                //Console.WriteLine(subreader.NodeType + " " + subreader.Name);
                                switch (subreader.Name)
                                {
                                    case "type":
                                        string type = reader.GetAttribute("xlink:type");
                                        string href  = reader.GetAttribute("xlink:href");
                                        g.Type = href;
                                        Console.WriteLine("GRAPH Type: xlink:type: " + type + " xlink:href " + href);
                                        break;
                                    case "node":
                                        if (subreader.NodeType == XmlNodeType.Element)
                                        {
                                            Node node = new Node(reader.GetAttribute("id"));
                                            List<Node> nodes = new List<Node>();
                                            nodes.Add(node);
                                            foreach (Node node1 in nodes)
                                            {
                                                Console.WriteLine(node);
                                            }

                                            XmlReader nodeReader = subreader.ReadSubtree();
                                            while (nodeReader.Read())
                                            {
                                                switch (nodeReader.Name)
                                                {
                                                    case "type":
                                                        node.Type = nodeReader.GetAttribute("xlink:href");
                                                        string nodeType = reader.GetAttribute("xlink:type");
                                                        Console.WriteLine("Node Type: xlink:type: " + nodeType + " xlink:href " + node.Type);
                                                        break;
                                                    case "attr":
                                                        //Attr attr = new Attr(reader.GetAttribute("name"), reader.GetAttribute("id"), reader.GetAttribute("kind"));
                                                        //List<Attr> attrs = new List<Attr>();
                                                        //attrs.Add(attr);
                                                        ParseAttr(nodeReader);
                                                        break;
                                                    case "graph":
                                                        //LoadGraph();
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    case "edge":
                                        if (subreader.NodeType == XmlNodeType.Element)
                                        {
                                            Edge edge = new Edge(reader.GetAttribute("id"), reader.GetAttribute("from"), reader.GetAttribute("to"), reader.GetAttribute("fromorder"), reader.GetAttribute("toorder"), Convert.ToBoolean(reader.GetAttribute("isdirected")));
                                            List<Edge> edges = new List<Edge>();
                                            edges.Add(edge);
                                            foreach (Edge node1 in edges)
                                            {
                                                Console.WriteLine(edge);
                                            }

                                            XmlReader nodeReader = subreader.ReadSubtree();
                                            while (nodeReader.Read())
                                            {
                                                switch (nodeReader.Name)
                                                {
                                                    case "type":
                                                        edge.Type = nodeReader.GetAttribute("xlink:href");
                                                        string nodeType = reader.GetAttribute("xlink:type");
                                                        Console.WriteLine("EDGE Type: xlink:type: " + nodeType + " xlink:href " + edge.Type);
                                                        break;
                                                    case "attr":
                                                        //Attr attr = new Attr(reader.GetAttribute("name"), reader.GetAttribute("id"), reader.GetAttribute("kind"));
                                                        //List<Attr> attrs = new List<Attr>();
                                                        //attrs.Add(attr);
                                                        ParseAttr(nodeReader);
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    case "rel":
                                        if (subreader.NodeType == XmlNodeType.Element)
                                        {
                                            Rel rel = new Rel(reader.GetAttribute("id"), Convert.ToBoolean(reader.GetAttribute("isdirected")));
                                            Console.WriteLine(rel);

                                            XmlReader relReader = subreader.ReadSubtree();
                                            while (relReader.Read())
                                            {
                                                switch (relReader.Name)
                                                {
                                                    case "type":
                                                        rel.Type = relReader.GetAttribute("xlink:href");
                                                        string nodeType = reader.GetAttribute("xlink:type");
                                                        Console.WriteLine("REL Type: xlink:type: " + nodeType + " xlink:href " + rel.Type);
                                                        break;
                                                    case "attr":
                                                        //Attr attr = new Attr(reader.GetAttribute("name"), reader.GetAttribute("id"), reader.GetAttribute("kind"));
                                                        //List<Attr> attrs = new List<Attr>();
                                                        //attrs.Add(attr);
                                                        ParseAttr(relReader);
                                                        break;
                                                    case "graph":
                                                        break;
                                                    case "relend":
                                                        break;
                                                }
                                            }

                                        }
                                        break;
                                    
                                }
                            }
                            Console.WriteLine("____________________");
                            break;
                        
                        
                    }
                    
                }
                
            }
        }
    }
        
}
