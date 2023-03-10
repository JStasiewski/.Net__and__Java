using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    internal class Item
    {
        Random rnd = new Random();

        int weight;
        int price;
        int id;

        public Item() 
        {
            weight = rnd.Next(1,30);
            price = rnd.Next(1,30);
        }
        public int getWeight()
        {
            return weight;
        }

        public int getPrice()
        {
            return price; 
        }
        public void setId(int id)
        {
            this.id = id;
        }
        public int getId()
        {
            return id;
        }
    }
}
