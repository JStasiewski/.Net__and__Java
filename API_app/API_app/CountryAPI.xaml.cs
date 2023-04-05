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

            if (json != "No country found. Ty again!")
            {
                var country = JsonSerializer.Deserialize<Country[]>(json);

                ListBox1.Items.Clear();                                         // add checkboxes conditions if(checked) listbox1.items.add ...
                ListBox1.Items.Add("Name : " + country[0].name.common);
                ListBox1.Items.Add("Region : " + country[0].region);
                ListBox1.Items.Add("Capitol : " + country[0].capital[0]);
                
                Uri img = new Uri(country[0].flags.png); // converting string to bitmap
                BitmapImage imageSource = new BitmapImage(img);
                Photo.Source = imageSource;
            }
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
