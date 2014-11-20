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
        List<Graph> Graph;

        public GXL()
        {
            Graph = new List<Graph>();
        }

        public void AddGraph(Graph graph)
        {
            Graph.Add(graph);
        }

        public override string ToString()
        {
            string result = "";
            foreach (Graph graphs in Graph)
            {
                result += graphs;
            }
            result += "------";
            return result;
        }
    }
}
