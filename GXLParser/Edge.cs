using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXLParser
{
    class Edge
    {
        private string ID;
        public string From { get; set; }
        public string To { get; set; }
        private string FromOrder;
        private string ToOrder;
        private bool IsDirected;
        public string Type { get; set; }
        private List<Attr> Attr;
        private List<Graph> Graph;

        public Edge(string id, string from, string to, string fromorder, string toorder, bool isdirected)
        {
            this.ID = id;
            this.From = from;
            this.To = to;
            this.FromOrder = fromorder;
            this.ToOrder = toorder;
            this.IsDirected = isdirected;
            Graph = new List<Graph>();
            Attr = new List<Attr>();
        }

        public void listOfEdges(Edge edge)
        {
            List<Edge> edges = new List<Edge>();
            edges.Add(edge);
            foreach (Edge node1 in edges)
            {
                Console.WriteLine(edge);
            }

        }

        public void AddGraph(Graph graph)
        {
            Graph.Add(graph);
        }


        public void AddAttr(Attr atr)
        {
            Attr.Add(atr);
        }

        public string getList()
        {
            string list = "";
            if (Graph.Count != 0)
            {
                foreach (Graph g1 in Graph)
                {
                    list += "\n";
                    list += "List pro vnořený graf " + g1.ID + " v edži jest: \n";
                    list += g1.getList();
                }
            }

            return list;
        }

        public override string ToString()
        {
            string result = "EDGE ID: " + ID + " from: " + this.From + " to: " + this.To + " fromOrder: " + this.FromOrder + " toOrder: " + this.ToOrder + " isDirected: " + this.IsDirected;
            foreach (Attr attrs in Attr)
            {
                result += attrs;
            }

            foreach (Graph graphs in Graph)
            {
                result += graphs.getList();
                result += graphs;
            }

            return result;
        }
    }
}
