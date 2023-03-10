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
        int will_fit;

        public int getWeight()
        {
            return weight = rnd.Next(1,30);
        }

        public int getPrice()
        {
            return price= rnd.Next(1,30); 
        }
    }
}
