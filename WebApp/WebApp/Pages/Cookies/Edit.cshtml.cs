using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Pages.Cookies;

namespace WebApp.Pages.Cookies
{
    public class EditModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public EditModel(WebApp.Data.WebAppContext context)
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

            var cookies =  await _context.Cookies.FirstOrDefaultAsync(m => m.ID == id);
            if (cookies == null)
            {
                return NotFound();
            }
            Cookies = cookies;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Cookies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CookiesExists(Cookies.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CookiesExists(int id)
        {
          return (_context.Cookies?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
