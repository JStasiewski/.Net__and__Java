using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_app
{
    internal class Set : DbContext
    {
        public DbSet<CountryDB> Countries { get; set; }
        public Set() 
        {
            Database.EnsureCreated();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(@"Data Source=Country.db");
    }

    internal class CountryDB
    {
        [Key]
        public int Id { get; set; }
        public string Json { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Capital { get; set; }
        public string Currencies { get; set; }
        public byte[] Image { get; set; }

    }
}
