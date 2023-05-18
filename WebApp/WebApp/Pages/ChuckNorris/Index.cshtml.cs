using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.ChuckNorris
{
    public class IndexModel : PageModel
    {
        public string Joke { get; set; }

        public async Task OnGet()
        {
            ChuckNorris chuckNorris = new ChuckNorris();
            Joke = await chuckNorris.GetRandomJokeAsync();
        }
    }
}
