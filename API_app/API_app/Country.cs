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
        public string region { get; set; }
        public string Anwser()
        {
            return $"{this.name.common}: {this.region}";
        }

        public Country()
        {
            this.name.common = "";
            this.region = "";
        }
    }
    internal class Name
    {
        public string common { get; set; }
    }
}
