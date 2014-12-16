using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXLParser
{
    class Attr
    {
        private string ID;
        private string Name;
        private string Kind;
        private List<Attr> Attrs;

        private string value;


        public Attr(string name, string id, string kind)
        {
            this.ID = id;
            this.Name = name;
            this.Kind = kind;
            Attrs = new List<Attr>();
        }

        public void setValue(string val)
        {
            value = val;
        }

        public void AddAttr(Attr atr)
        {
            Attrs.Add(atr);
        }

        public override string ToString()
        {
            string result = this.Name + " " + this.ID + " " + this.Kind;
            foreach (Attr attrs in Attrs)
            {
                result += attrs;
            }
            return result;
        }

        public string getData()
        {
            string result = "";
            result += this.Name + ":" + this.value;

            return result;
        }
    }
}
