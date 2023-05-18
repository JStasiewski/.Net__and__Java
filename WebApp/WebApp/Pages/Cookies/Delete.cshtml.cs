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
    public class DeleteModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public DeleteModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Cookies == null)
            {
                return NotFound();
            }
            var cookies = await _context.Cookies.FindAsync(id);

            if (cookies != null)
            {
                Cookies = cookies;
                _context.Cookies.Remove(Cookies);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
