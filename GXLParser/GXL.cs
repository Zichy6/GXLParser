using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GXLParser
{
    class GXL
    {
        private List<Graph> Graph;

        public GXL()
        {
            Graph = new List<Graph>();
        }

        public void AddGraph(Graph graph)
        {
            Graph.Add(graph);
        }

        //Vypis vsech grafu do listu sousednosti
        public void getListInfo()
        {
            foreach (Graph g in Graph)
            {
                Console.WriteLine("\nList sousednosti pro graf " + g.ID + " je:");
                Console.WriteLine(g.getList());
            }
        }

        public override string ToString()
        {
            string result = "";
            foreach (Graph graphs in Graph)
            {
                result += graphs;
            }
            return result;
        }
    }
}
