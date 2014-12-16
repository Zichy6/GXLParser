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
            XmlReaderSettings set = new XmlReaderSettings();
            set.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(@"C:\Users\Crossfire\Documents\Visual Studio 2012\Projects\GXLParser\GXLParser\Test2.gxl", set);
            ParseGXL(reader);
            
            gxl.getListInfo();
            Space();
            Console.WriteLine("Informace o hranách a uzlech:");
            Space();
            Console.WriteLine(gxl.getResultGraphs());

            Console.Read();
        }

        
        private static void ParseGXL(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "graph":
                            Graph g = new Graph(reader.GetAttribute("id"), reader.GetAttribute("role"), Convert.ToBoolean(reader.GetAttribute("edgeids")), reader.GetAttribute("edgemode"), Convert.ToBoolean(reader.GetAttribute("hypergraph")));
                            gxl.AddGraph(g);
                            Console.WriteLine(g);
                            XmlReader subreader = reader.ReadSubtree();
                            ParseGraph(subreader, g);
                            break;
                    }
                }
            }
        }

        private static void ParseGraph(XmlReader subreader, Graph g)
        {
            while (subreader.Read())
            {
                switch (subreader.Name)
                {
                    case "type":
                        string type = subreader.GetAttribute("xlink:type");
                        string href = subreader.GetAttribute("xlink:href");
                        g.Type = href;
                        Console.WriteLine("GRAPH Type: xlink:type: " + type + " xlink:href " + href);
                        break;
                    case "node":
                        if (subreader.NodeType == XmlNodeType.Element)
                        {
                            Node node = new Node(subreader.GetAttribute("id"));
                            g.AddNode(node);
                            node.listOfNodes(node);
                            XmlReader nodeReader = subreader.ReadSubtree();
                            while (nodeReader.Read())
                            {
                                switch (nodeReader.Name)
                                {
                                    case "type":
                                        node.Type = nodeReader.GetAttribute("xlink:href");
                                        string nodeType = subreader.GetAttribute("xlink:type");
                                        Console.WriteLine("Node Type: xlink:type: " + nodeType + " xlink:href " + node.Type);
                                        break;
                                    case "attr":                                        
                                        node.AddAttr(ParseAttr(nodeReader));
                                        break;
                                    case "graph":
                                        Graph gNode = new Graph(subreader.GetAttribute("id"), subreader.GetAttribute("role"), Convert.ToBoolean(subreader.GetAttribute("edgeids")), subreader.GetAttribute("edgemode"), Convert.ToBoolean(subreader.GetAttribute("hypergraph")));
                                        ParseGraph(nodeReader, gNode);
                                        node.AddGraph(gNode);
                                        Space();
                                        Console.WriteLine("Graph in da Node: ");
                                        Console.WriteLine(gNode);
                                        Space();
                                        break;
                                }
                            }
                            node.getData();
                        }
                        break;
                    case "edge":
                        if (subreader.NodeType == XmlNodeType.Element)
                        {
                            Edge edge = new Edge(subreader.GetAttribute("id"), subreader.GetAttribute("from"), subreader.GetAttribute("to"), subreader.GetAttribute("fromorder"), subreader.GetAttribute("toorder"), Convert.ToBoolean(subreader.GetAttribute("isdirected")));
                            g.AddEdge(edge);
                            edge.listOfEdges(edge);
                            XmlReader edgeReader = subreader.ReadSubtree();
                            while (edgeReader.Read())
                            {
                                switch (edgeReader.Name)
                                {
                                    case "type":
                                        edge.Type = edgeReader.GetAttribute("xlink:href");
                                        string nodeType = edgeReader.GetAttribute("xlink:type");
                                        Console.WriteLine("EDGE Type: xlink:type: " + nodeType + " xlink:href " + edge.Type);
                                        break;
                                    case "attr":
                                        ParseAttr(edgeReader);
                                        break;
                                    case "graph":
                                        Graph gEdge = new Graph(subreader.GetAttribute("id"), subreader.GetAttribute("role"), Convert.ToBoolean(subreader.GetAttribute("edgeids")), subreader.GetAttribute("edgemode"), Convert.ToBoolean(subreader.GetAttribute("hypergraph")));
                                        ParseGraph(edgeReader, gEdge);
                                        edge.AddGraph(gEdge);
                                        Space();
                                        Console.WriteLine("Graph in da Edge: ");
                                        Console.WriteLine(gEdge);
                                        Space();
                                        break;
                                }
                            }
                        }
                        break;
                    case "rel":
                        if (subreader.NodeType == XmlNodeType.Element)
                        {
                            Rel rel = new Rel(subreader.GetAttribute("id"), Convert.ToBoolean(subreader.GetAttribute("isdirected")));
                            Console.WriteLine(rel);

                            XmlReader relReader = subreader.ReadSubtree();
                            while (relReader.Read())
                            {
                                switch (relReader.Name)
                                {
                                    case "type":
                                        rel.Type = relReader.GetAttribute("xlink:href");
                                        string nodeType = subreader.GetAttribute("xlink:type");
                                        Console.WriteLine("REL Type: xlink:type: " + nodeType + " xlink:href " + rel.Type);
                                        break;
                                    case "attr":
                                        ParseAttr(relReader);
                                        break;
                                    case "graph":
                                        Graph gRel = new Graph(subreader.GetAttribute("id"), subreader.GetAttribute("role"), Convert.ToBoolean(subreader.GetAttribute("edgeids")), subreader.GetAttribute("edgemode"), Convert.ToBoolean(subreader.GetAttribute("hypergraph")));
                                        rel.AddGraph(gRel);
                                        ParseGXL(relReader);
                                        Space();
                                        Console.WriteLine("Graph in da Rel: ");
                                        Console.WriteLine(gRel);
                                        Space();
                                        break;
                                    case "relend":
                                        if (relReader.NodeType == XmlNodeType.Element)
                                        {
                                            Relend relend = new Relend(relReader.GetAttribute("target"), relReader.GetAttribute("role"), relReader.GetAttribute("direction"), relReader.GetAttribute("startorder"), relReader.GetAttribute("endorder"));
                                            rel.AddRelend(relend);
                                            Console.WriteLine(relend);
                                            XmlReader relendReader = relReader.ReadSubtree();
                                            while (relendReader.Read())
                                            {
                                                switch (relendReader.Name)
                                                {
                                                    case "attr":
                                                        ParseAttr(relReader);
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                }
            }
        }


        private static Attr ParseAttr(XmlReader reader)
        {
            Attr attr = new Attr(reader.GetAttribute("name"), reader.GetAttribute("id"), reader.GetAttribute("kind"));
            if (reader.NodeType == XmlNodeType.Element)
            {
                
                AttrValues val = new AttrValues();
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
                            if (attrReader.NodeType == XmlNodeType.Element && attrReader.NodeType != XmlNodeType.Whitespace)
                            {
                                //Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsInt());
                                attr.setValue(attrReader.ReadElementContentAsString());
                            }
                            break;
                        case "string":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                //Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsString());
                                attr.setValue(attrReader.ReadElementContentAsString());
                            }
                            break;
                        case "bool":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                //Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsBoolean());
                                attr.setValue(attrReader.ReadElementContentAsString());
                            }
                            break;
                        case "float":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                               // Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsFloat());
                                attr.setValue(attrReader.ReadElementContentAsString());
                            }
                            break;
                        case "enum":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                //Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsString());
                                attr.setValue(attrReader.ReadElementContentAsString());
                            }
                            break;
                        case "locator":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                string type = reader.GetAttribute("xlink:type");
                                string href = reader.GetAttribute("xlink:href");
                                val.Type = href;
                                Console.WriteLine("Locator type: xlink:type: " + type + " href: xlink:href " + href);
                            }
                            break;
                        case "seq":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                //AttrValues attrVal = new AttrValues();
                                //attrVal.AddAttrValues(attrVal);
                                //ParseAttr(reader);
                                //Space();
                                Console.WriteLine("\tVnořený attr seq, který obsahuje: ");
                                //counter++;
                            }
                            else if (attrReader.NodeType == XmlNodeType.EndElement)
                            {
                                Console.WriteLine("\tKonec sequ");
                                Space();
                            }
                            break;
                        case "set":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                Space();
                                //AttrValues attrValSet = new AttrValues();
                                //attrValSet.AddAttrValues(attrValSet);
                                //ParseAttr(reader);
                                Console.WriteLine("\tVnořený attr set, který obsahuje: ");
                                //counter++;
                            }
                            else if (attrReader.NodeType == XmlNodeType.EndElement)
                            {
                                Console.WriteLine("\tKonec setu");
                                Space();
                            }
                            break;
                        case "bag":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                Space();
                                Console.WriteLine("\tVnořený attr:  bag, který obsahuje: ");
                                //ParseAttr(reader);

                            }
                            else if (attrReader.NodeType == XmlNodeType.EndElement)
                            {
                                Console.WriteLine("\tKonec bagu");
                                Space();
                            }
                            break;
                        case "tup":
                            if (attrReader.NodeType == XmlNodeType.Element)
                            {
                                Space();
                                Console.WriteLine("\tVnořený attr tup, který obsahuje: ");
                                XmlReader attrSubReader = attrReader.ReadSubtree();
                                while (attrSubReader.Read())
                                {
                                    switch (attrSubReader.Name)
                                    {
                                        case "int":
                                            if (attrSubReader.NodeType == XmlNodeType.Element && attrSubReader.NodeType != XmlNodeType.Whitespace)
                                            {
                                                //Console.WriteLine("ATTR type: " + attrReader.Name + " VALUE: " + attrReader.ReadElementContentAsInt());
                                                attr.setValue(attrReader.ReadElementContentAsString());
                                            }
                                            break;
                                    }
                                }
                                //ParseAttr(reader);
                                //counter++;
                            }
                            else if (attrReader.NodeType == XmlNodeType.EndElement)
                            {
                                Console.WriteLine("\tKonec tupu");
                                Space();
                            }
                            break;
                    }
                }
                
            }
            return attr;
        }

        private static void Space()
        {
            Console.WriteLine("________________________________________________________________________________");
        }
    }
        
}
