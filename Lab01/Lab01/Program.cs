using Lab01;
using System;

namespace Lab01
{
    class Program
    {
        static void Naive(List<Item> lits,Backpack backpack)
        {
            foreach (var item in lits)
            {
                if(!backpack.isFull())
                {
                    Console.WriteLine(item.getWeight() + "   " + item.getPrice());
                    backpack.addToBackpack(item.getWeight(),item.getPrice());
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Price colected : " + backpack.PriceInBackpack());
            Console.WriteLine("Capacity : " + backpack.Capacity());
        }
        static void Main(string[] args)
        {
            Backpack backpack = new Backpack();

            Console.WriteLine("Backpack capacity : " + backpack.Capacity());
            Console.WriteLine("----------------------");

            List<Item> list = new List<Item>();

            for(int i = 0; i < 10; i++)
            {
                Item item = new Item();
                list.Add(item);
                list[i].setId(i);
                Console.WriteLine(i + " Weight: " + list[i].getWeight());
                Console.WriteLine(i + " Price:  " + list[i].getPrice());
                Console.WriteLine(i + " ID:  " + list[i].getId());
                Console.WriteLine("");
            }

            Naive(list, backpack);
        }
    }
}