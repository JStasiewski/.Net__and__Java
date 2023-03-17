using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    internal class Item
    {
        Random rnd = new Random();

        int _weight;
        int _price;
        int _id;

        public Item() 
        {
            _weight = rnd.Next(1,30);
            _price = rnd.Next(1,30);
        }
        public int getWeight()
        {
            return _weight;
        }

        public int getPrice()
        {
            return _price; 
        }
        public void setId(int id)
        {
            this._id = id;
        }
        public int getId()
        {
            return _id;
        }
    }
}
