using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Pages.ChuckNorris
{
    public class ChuckNorris
    {
        private readonly HttpClient _httpClient;

        public ChuckNorris()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.chucknorris.io/");
        }

        public async Task<string> GetRandomJokeAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("jokes/random");
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                dynamic joke = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                return joke.value;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the request
                // Display an error message or take appropriate action
                return "Failed to retrieve joke: " + ex.Message;
            }
        }
    }
}
