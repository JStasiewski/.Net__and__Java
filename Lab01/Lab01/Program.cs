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
                    backpack.addToBackpack(item.getWeight(),item.getPrice(),item.getId());
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Price colected : " + backpack.PriceInBackpack());
            Console.WriteLine("Capacity : " + backpack.Capacity());
            Console.WriteLine("Items ID's : " + backpack.ItemsId());
        }
        static void Main(string[] args)
        {
            Backpack backpack = new Backpack();
            int itemQ = 10;

            Console.WriteLine("Default Backpack capacity : " + backpack.Capacity() + "   Item quantity : " + itemQ);
            Console.WriteLine("Set other capacity and items quantity? (Y/N)");
            string choise = Console.ReadLine();
            if (choise == "Y")
            {
                Console.WriteLine("Set custom backpack capacity:");
                int customCapacity = Convert.ToInt32(Console.ReadLine());
                backpack.setCapacity(customCapacity);
                Console.WriteLine("Set custom items quantity:");
                itemQ  = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("----------------------");

            List<Item> list = new List<Item>();

            for(int i = 0; i < itemQ; i++)
            {
                Item item = new Item();
                list.Add(item);
                list[i].setId(i);
                Console.WriteLine($"Weight: {list[i].getWeight(),2}" + $"   Price: {list[i].getPrice(),2}" + $"   ID: {list[i].getId(),2}");
            }
            Console.WriteLine("");
            Naive(list, backpack);
        }
    }
}