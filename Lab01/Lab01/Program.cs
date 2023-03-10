using Lab01;
using System;

namespace Lab01
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Item item = new Item();
            Backpack backpack = new Backpack();

            Console.WriteLine("Backpack capacity : " + backpack.Capacity());

            List<Item> list = new List<Item>();

            for(int i = 0; i <= 30; i++)
            {
                list.Add(item);
                Console.WriteLine(i + " Weight: " + list[i].getWeight());
                Console.WriteLine(i + " Price:  " + item.getPrice());
                Console.WriteLine("");
            }
        }
    }
}