using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXLParser
{
    class Graph
    {
        private string ID;
        private bool Edgeids;
        private string Edgemode;
        private bool Hypergraph;

        public string Type { get; set; }
        List<Attr> Attr;
        List<Node> Node;
        List<Edge> Edge;
        List<Rel> Rel;

        public Graph(string id, bool edgeids, string edgemode, bool hypergraph)
        {
            ID = id;
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

        public override string ToString()
        {
            string result = "GRAPH ID: " + this.ID + " edgeids: " + this.Edgeids + " edgemode:" + this.Edgemode + " hypergraph: " + this.Hypergraph + this.Type;
            
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
    }
}
