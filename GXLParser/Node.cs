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

        public string getNodeID()
        {
            return ID;
        }

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

        public void listOfNodes(Node node)
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(node);
            foreach (Node node1 in nodes)
            {
                Console.WriteLine(node);
            }

        }

        public string getList()
        {
            string list = "";
            if (Graph.Count != 0)
            {
                foreach (Graph g1 in Graph)
                {
                    list += "\n";
                    list += "List pro vnořený graf " + g1.ID + " v nodu jest: \n";
                    list += g1.getList();
                }
            }

            return list;
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

        public string getData()
        {
            string result = "[";
            result += this.ID + ":";
            foreach (Attr a in Attr)
            {
                result += a.getData() + ";";
            }
            result += "]\n";

            return result;
        }

    }
}
