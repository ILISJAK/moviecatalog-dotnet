using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalog.Pages_Movies
{
    public class FilterModel : PageModel
    {
        private readonly MovieCatalogContext _context;

        public FilterModel(MovieCatalogContext context)
        {
            _context = context;
        }

        public IList<Movie> Movies { get; set; } = new List<Movie>();

        public async Task OnGetAsync(string genre)
        {
            if (!string.IsNullOrEmpty(genre))
            {
                Movies = await _context.Movies
                    .Where(m => m.Genre.Contains(genre))
                    .ToListAsync();
            }
            else
            {
                Movies = await _context.Movies.ToListAsync();
            }
        }
    }
}
