using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_app
{
    internal class Country
    {
        public Name name { get; set; }
        public Flags flags { get; set; }
        public string region { get; set; }
        public Currencies currencies { get; set; }
        public override string ToString()
        {
            return $"{this.name.common}: {this.region}";
        }
    }
    internal class Name
    {
        public string common { get; set; }
        public string official { get; set; }
    }
    internal class Flags
    {
        public string png { get; set; }
    }
    internal class Currencies
    {
        public Pln PLN { get; set; }
    }

    public class Pln
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }
}