using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static void Naive(List<Item> lits, Backpack backpack)
        {
            foreach (var item in lits)
            {
                if (!backpack.isFull())
                {
                    backpack.addToBackpack(item.getWeight(), item.getPrice(), item.getId());
                }
                else
                {
                    break;
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Backpack backpack = new Backpack();

            List<Item> list = new List<Item>();

            backpack.setCapacity(int.Parse(BackpackC.Text));
            itemst.Items.Clear();
            for (int i = 0; i < int.Parse(ItemsQ.Text); i++)
            {
                Item item = new Item();
                list.Add(item);
                list[i].setId(i);
                itemst.Items.Add($"Weight: {list[i].getWeight(),2}" + $"   Price: {list[i].getPrice(),2}" + $"   ID: {list[i].getId(),2}");
            }


            Naive(list, backpack);
            result1.Text = "Price colected : " + backpack.PriceInBackpack().ToString();
            result2.Text = "Capacity left : " + backpack.Capacity().ToString();
            result3.Text = "Taken items ID's : " + backpack.ItemsId().ToString();
        }

        private void BackpackC_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(BackpackC.Text, out int result))
            {
                button.IsEnabled = true;
            }
            else
            {
                button.IsEnabled = false;
            }
        }

        private void ItemsQ_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(ItemsQ.Text, out int result))
            {
                button.IsEnabled = true;
            }
            else
            {
                button.IsEnabled = false;
            }
        }
    }
}
