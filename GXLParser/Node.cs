using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXLParser
{
    class Node
    {
        private string ID;
        public string Type { get; set; }
        private List<Attr> Attr;
        private List<Graph> Graph;

        public Node(string id)
        {
            ID = id;
            Graph = new List<Graph>();
            Attr = new List<Attr>();
        }

        public void AddAttr(Attr atr)
        {
            Attr.Add(atr);
        }

        public void AddGraph(Graph grp)
        {
            Graph.Add(grp);
        }

        public override string ToString()
        {
            string result = "NODE ID: " + ID + this.Type;

            foreach (Attr attrs in this.Attr)
            {
                result += attrs;
            }

            foreach (Graph graphs in this.Graph)
            {
                result += graphs;
            }
            return result; 
        }

    }
}
