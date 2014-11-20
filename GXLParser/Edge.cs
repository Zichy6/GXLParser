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
        private string From;
        private string To;
        private string FromOrder;
        private string ToOrder;
        private bool IsDirected;
        public string Type { get; set; }
        private List<Attr> Attr;

        public Edge(string id, string from, string to, string fromorder, string toorder, bool isdirected)
        {
            this.ID = id;
            this.From = from;
            this.To = to;
            this.FromOrder = fromorder;
            this.ToOrder = toorder;
            this.IsDirected = isdirected;
            Attr = new List<Attr>();
        }

        public override string ToString()
        {
            string result = "EDGE ID: " + ID + " from: " + this.From + " to: " + this.To + " fromOrder: " + this.FromOrder + " toOrder: " + this.ToOrder + " isDirected: " + this.IsDirected;
            foreach (Attr attrs in Attr)
            {
                result += attrs;
            }
            return result;
        }
    }
}
