using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalog.Pages_Movies
{
    public class SortedModel : PageModel
    {
        private readonly MovieCatalogContext _context;

        public SortedModel(MovieCatalogContext context)
        {
            _context = context;
        }

        public IList<Movie> Movies { get; set; } = new List<Movie>();

        public async Task OnGetAsync()
        {
            Movies = await _context.Movies
                .OrderBy(m => m.Title)
                .ToListAsync();
        }
    }
}
