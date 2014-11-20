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
        string Target;
        string Role;
        string Direction;
        string StartOrder;
        string EndOrder;
        /*Elementy*/
        List<Attr> Attr;

        public void AddAttr(Attr atr)
        {
            Attr.Add(atr);
        }

        public override string ToString()
        {
            return Target + " " + this.Role + " " + this.Direction + " " + this.StartOrder + " " + this.EndOrder + " " + this.Attr;
        }
    }
}
