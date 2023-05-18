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
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public DetailsModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

      public Cookies Cookies { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cookies == null)
            {
                return NotFound();
            }

            var cookies = await _context.Cookies.FirstOrDefaultAsync(m => m.ID == id);
            if (cookies == null)
            {
                return NotFound();
            }
            else 
            {
                Cookies = cookies;
            }
            return Page();
        }
    }
}
