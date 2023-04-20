using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
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
        //private Set set;
        public CountryAPI()
        {
            InitializeComponent();
            //set = new Set();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string json = await downloadData(TextBox.Text);

            if (json != "No country found. Ty again!")
            {
                var country = JsonSerializer.Deserialize<Country[]>(json);
                string capitolStr = "";

                ListBox1.Items.Clear();                                         // add checkboxes conditions if(checked) listbox1.items.add ...
                ListBox1.Items.Add("Name : " + country[0].name.common);
                ListBox1.Items.Add("Region : " + country[0].region);

                foreach (string cap in country[0].capital) capitolStr += cap + " ";
                ListBox1.Items.Add("Capitol : " + capitolStr);

                string pattern = @"\""([^\""]*)\""";

                MatchCollection matches = Regex.Matches(country[0].currencies.ToString(), pattern);

                string currStd = "";
                foreach (Match match in matches)
                {
                    currStd += match.Groups[1].Value + " ";
                }

                ListBox1.Items.Add("Currencies : " + currStd);
                
                Uri img = new Uri(country[0].flags.png); // converting string to bitmap
                BitmapImage imageSource = new BitmapImage(img);
                Photo.Source = imageSource;
            }
            else
            {
                ListBox1.Items.Clear();
                ListBox1.Items.Add("No country found! Try again!");
                Uri imgNot = new Uri("https://howfix.net/wp-content/uploads/2018/02/sIaRmaFSMfrw8QJIBAa8mA-article.png"); // converting string to bitmap
                BitmapImage imageSourceNot = new BitmapImage(imgNot);
                Photo.Source = imageSourceNot;
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
