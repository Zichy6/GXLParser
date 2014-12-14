using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXLParser
{
    class Rel
    {
        /*Atributy*/
        private string ID;
        private bool IsDirected;
        /*Elementy*/
        public string Type { get; set; }
        private List<Attr> Attr;
        private List<Graph> Graph;
        private List<Relend> Relend;

        public Rel(string id, bool isDirected)
        {
            this.ID = id;
            this.IsDirected = isDirected;
            Attr = new List<Attr>();
            Graph = new List<Graph>();
            Relend = new List<Relend>();
        }

        public void AddAttr(Attr atr)
        {
            Attr.Add(atr);
        }

        public void AddGraph(Graph graph)
        {
            Graph.Add(graph);
        }

        public void AddRelend(Relend relend)
        {
            Relend.Add(relend);
        }

        public override string ToString()
        {
            string result = "REL ID: " + ID + " idDirected: " + this.IsDirected + " " + this.Type;
            foreach (Attr attrs in this.Attr)
            {
                result += attrs;
            }
            foreach (Relend relends in Relend)
            {
                result += relends;
            }
            foreach (Graph graphs in Graph)
            {
                result += graphs;
            }
            return result;
        }
    }
}
