using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Pages.Cookies;

namespace WebApp.Pages.Cookies
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public IndexModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public IList<Cookies> Cookies { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Cookies != null)
            {
                Cookies = await _context.Cookies.ToListAsync();
            }
        }
    }
}
