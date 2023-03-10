using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    internal class Backpack
    {
        int capacity = 30;
        int priceInBackpack = 0;
        bool full = false;
        
        public int Capacity()
        { 
            return capacity;
        }
        public int PriceInBackpack()
        {
            return priceInBackpack;
        }
        public void addToBackpack(int weight,int price)
        {
            if (!full)
            {
                if (capacity - weight > 0)
                {
                    capacity -= weight;
                    priceInBackpack += price;
                }
                else if(capacity - weight == 0)
                {
                    capacity -= weight;
                    full = true;
                }
            }
        }

        public bool isFull()
        {
            return full;
        }
    }
}
