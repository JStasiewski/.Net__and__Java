using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Path = System.IO.Path;

namespace API_app
{
    public partial class CountryAPI : Window
    {
        public CountryAPI()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string countryName = TextBox.Text;

            using (var context = new Set())
            {
                var existingCountry = context.Countries.FirstOrDefault(c => c.Name == countryName);
                if (existingCountry != null)
                {
                    ListBox1.Items.Clear();
                    ListBox1.Items.Add("Data from DB");
                    ListBox1.Items.Add("Name : " + existingCountry.Name);
                    ListBox1.Items.Add("Region : " + existingCountry.Region);
                    ListBox1.Items.Add("Capitol : " + existingCountry.Capital);
                    ListBox1.Items.Add("Currencies : " + existingCountry.Currencies);

                    byte[] imageData = existingCountry.Image;
                    BitmapImage imageSource = new BitmapImage();
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        imageSource.BeginInit();
                        imageSource.CacheOption = BitmapCacheOption.OnLoad;
                        imageSource.StreamSource = ms;
                        imageSource.EndInit();
                    }
                    Photo.Source = imageSource;
                    return;
                }
            }

            string json = await downloadData(countryName);

            if (json != "No country found. Try again!" && json != "")
            {
                var country = JsonSerializer.Deserialize<Country[]>(json);
                string capitalStr = "";

                foreach (string cap in country[0].capital) capitalStr += cap + " ";

                string pattern = @"\""([^\""]*)\""";
                string currStd = "";
                MatchCollection matches = Regex.Matches(country[0].currencies.ToString(), pattern);

                foreach (Match match in matches)
                {
                    currStd += match.Groups[1].Value + " ";
                }

                ListBox1.Items.Clear();
                ListBox1.Items.Add("Name : " + country[0].name.common);
                ListBox1.Items.Add("Region : " + country[0].region);
                ListBox1.Items.Add("Capitol : " + capitalStr);
                ListBox1.Items.Add("Currencies : " + currStd);

                Uri imgUri = new Uri(country[0].flags.png);

                using (var client = new WebClient())
                {
                    byte[] imageData = await client.DownloadDataTaskAsync(imgUri);

                    using (var context = new Set())
                    {
                        var newCountry = new CountryDB
                        {
                            Json = json,
                            Name = country[0].name.common,
                            Region = country[0].region,
                            Capital = capitalStr,
                            Currencies = currStd,
                            Image = imageData
                        };
                        context.Countries.Add(newCountry);
                        context.SaveChanges();
                    }

                    BitmapImage imageSource = new BitmapImage();
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        imageSource.BeginInit();
                        imageSource.CacheOption = BitmapCacheOption.OnLoad;
                        imageSource.StreamSource = ms;
                        imageSource.EndInit();
                    }
                    Photo.Source = imageSource;
                }
            }
            else
            {
                ListBox1.Items.Clear();
                ListBox1.Items.Add("No country found! Try again!");
                Uri imgNot = new Uri("https://howfix.net/wp-content/uploads/2018/02/sIaRmaFSMfrw8QJIBAa8mA-article.png"); // converting string to bitmap
                BitmapImage imageSource;
            }
        }

        private async Task<string> downloadData(string name)
        {
            using (var context = new Set())
            {
                var existingCountry = await context.Countries.FirstOrDefaultAsync(c => c.Name == name);

                if (existingCountry != null)
                {
                    return existingCountry.Json;
                }
            }

            try
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
                    return "No country found. Try again!";
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return "";
            }
        }

    }
}
