using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXLParser
{
    class Graph
    {
        public string ID { get; set; }
        private string Role;
        private bool Edgeids;
        private string Edgemode;
        private bool Hypergraph;

        public string Type { get; set; }
        private List<Attr> Attr;
        private List<Node> Node;
        private List<Edge> Edge;
        private List<Rel> Rel;

        public Graph(string id, string role, bool edgeids, string edgemode, bool hypergraph)
        {
            ID = id;
            Role = role;
            Edgeids = edgeids;
            Edgemode = edgemode;
            Hypergraph = hypergraph;
            Attr = new List<Attr>();
            Node = new List<Node>();
            Edge = new List<Edge>();
            Rel = new List<Rel>();
        }

        public void AddAttr(Attr atr)
        {
            Attr.Add(atr);
        }

        public void AddNode(Node node)
        {
            Node.Add(node);
        }

        public void AddEdge(Edge edge)
        {
            Edge.Add(edge);
        }

        public void AddRel(Rel rel)
        {
            Rel.Add(rel);
        }

        public string getList()
        {
            string list = "";

            foreach (Node n in Node)
            {
                list += n.getNodeID();
                foreach (Node n2 in Node)
                {
                    if (spojenka(n.getNodeID(), n2.getNodeID()))
                    {
                        list += " -> " + n2.getNodeID();
                    }
                }
                list += "\n";
            }

            foreach (Node n in Node)
            {
                list += n.getList();
            }

            foreach (Edge e in Edge)
            {

                list += e.getList();
                
            }

            return list;
        }

        public bool spojenka(string id1, string id2)
        {
            // melo by fungovat
            string directed = "directed";
            bool Ok = false;
            foreach (Edge edges in Edge)
            {
                //pro orientovany graf
                if (Edgemode == directed)
                {
                    if (id1 == edges.From && id2 == edges.To)
                        Ok = true;
                }
                //pro neorientovany graf
                else
                {
                    if (id1 == edges.From && id2 == edges.To || id1 == edges.To && id2 == edges.From)
                        Ok = true;
                }
            }
            return Ok;
        }

        public override string ToString()
        {
            string result = "Našel jsem GRAF: \n";
            result += "jeho ID: " + this.ID + " edgeids: " + this.Edgeids + " role: " + this.Role + " edgemode:" + this.Edgemode + " hypergraph: " + this.Hypergraph + this.Type;
            
            foreach (Attr attrs in this.Attr)
            {
                result += attrs;
            }
            foreach (Node nodes in Node)
            {
                result += nodes;
            }
            foreach (Edge edges in Edge)
            {
                result += edges;
            }
            foreach (Rel rels in Rel)
            {
                result += rels;
            }
            return result;
        }

        public string getResult()
        {
            string result = "";
            foreach (Node nodes in Node)
            {
                result += nodes.getData();
            }

            foreach (Edge edges in Edge)
            {
                result += edges.getData();
            }

            return result;
        }
    }
}
