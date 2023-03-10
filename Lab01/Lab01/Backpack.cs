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
        string itemsId = "";
        bool full = false;
        
        public void setCapacity(int capacity)
        {
            this.capacity = capacity;
        }
        public int Capacity()
        { 
            return capacity;
        }
        public int PriceInBackpack()
        {
            return priceInBackpack;
        }
        public string ItemsId()
        {
            return itemsId;
        }
        public void addToBackpack(int weight,int price, int id)
        {
            if (!full)
            {
                if (capacity - weight > 0)
                {
                    capacity -= weight;
                    priceInBackpack += price;
                    itemsId += id.ToString() + "   ";
                }
                else if(capacity - weight == 0)
                {
                    capacity -= weight;
                    priceInBackpack += price;
                    itemsId += id.ToString() + "   ";
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
