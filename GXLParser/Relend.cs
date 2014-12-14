using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXLParser
{
    class Relend
    {
        /*Atributy*/
        private string Target;
        private string Role;
        private string Direction;
        private string StartOrder;
        private string EndOrder;
        /*Elementy*/
        private List<Attr> Attr;

        public Relend(string target, string role, string direction, string startOrder, string endOrder)
        {
            this.Target = target;
            this.Role = role;
            this.Direction = direction;
            this.StartOrder = startOrder;
            this.EndOrder = endOrder;
            Attr = new List<Attr>();
        }

        public void AddAttr(Attr atr)
        {
            Attr.Add(atr);
        }

        public override string ToString()
        {
            string result = "___________\n" + "RELEND: \n" +  "Target: " + Target + " Role: " + this.Role + " Direction: " + this.Direction + " StartOrder: " + this.StartOrder + " EndOrder: " + this.EndOrder;
            foreach (Attr attrs in Attr)
            {
                result += attrs;
            }
            return result;
        }
    }
}
