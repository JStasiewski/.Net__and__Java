using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Pages.Cookies;

namespace WebApp.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext (DbContextOptions<WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<WebApp.Pages.Cookies.Cookies> Cookies { get; set; } = default!;
    }
}
