using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    internal class Backpack
    {
        int _capacity = 30;
        int _priceInBackpack = 0;
        string _itemsId = "";
        bool _full = false;
        
        public void setCapacity(int capacity)
        {
            this._capacity = capacity;
        }
        public int Capacity()
        { 
            return _capacity;
        }
        public int PriceInBackpack()
        {
            return _priceInBackpack;
        }
        public string ItemsId()
        {
            return _itemsId;
        }
        public void addToBackpack(int weight,int price, int id)
        {
            if (!_full)
            {
                if (_capacity - weight > 0)
                {
                    _capacity -= weight;
                    _priceInBackpack += price;
                    _itemsId += id.ToString() + "   ";
                }
                else if(_capacity - weight == 0)
                {
                    _capacity -= weight;
                    _priceInBackpack += price;
                    _itemsId += id.ToString() + "   ";
                    _full = true;
                }
            }
        }

        public bool isFull()
        {
            return _full;
        }
    }
}
