using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Models;
using MovieCatalog.Data;

namespace MovieCatalog.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly MovieCatalogContext _context;

        public IndexModel(MovieCatalogContext context)
        {
            _context = context;
            Movie = new List<Movie>();
        }

        public IList<Movie> Movie { get; set; }

        public async Task OnGetAsync()
        {
            Movie = await _context.Movies.ToListAsync();
        }
    }
}
