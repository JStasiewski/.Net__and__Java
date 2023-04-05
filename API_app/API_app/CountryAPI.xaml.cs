using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace API_app
{
    /// <summary>
    /// Interaction logic for CountryAPI.xaml
    /// </summary>
    public partial class CountryAPI : Window
    {
        public CountryAPI()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string json = await downloadData(TextBox.Text);
            TextBlock.Text = json;


        }

        private async Task<string> downloadData(string name)
        {
            HttpClient client = new HttpClient();
            string call = $"https://restcountries.com/v3.1/name/{name}";
            HttpResponseMessage response = await client.GetAsync(call);
            if (response.IsSuccessStatusCode)
            {
                string json = await client.GetStringAsync(call);
                return json;
            }
            else
            {
                return "No country found. Ty again!";
            }
        }
    }
}
