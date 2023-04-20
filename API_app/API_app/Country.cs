using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_app
{
    internal class Country
    {
        [Key]
        public int Id { get; set; }
        public Name name { get; set; }
        public Flags flags { get; set; }
        public string region { get; set; }
        [NotMapped]
        public string[] capital { get; set; }
        [NotMapped]
        public object currencies { get; set; }
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
}