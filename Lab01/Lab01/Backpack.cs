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
        
        public int Capacity()
        { 
                return capacity;
        }
        public int addToBackpack(int weight)
        {
            return capacity - weight;
        }
    }
}
