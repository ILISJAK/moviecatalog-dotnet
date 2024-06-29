using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MovieCatalog.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly MovieCatalogContext _context;

        public IndexModel(MovieCatalogContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movies.ToListAsync();
        }

        public async Task<IActionResult> OnGetFilteredAndSortedMoviesAsync(string genre, string sortOption)
        {
            var moviesQuery = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                moviesQuery = moviesQuery.Where(m => m.Genre.Contains(genre));
            }

            moviesQuery = sortOption switch
            {
                "title" => moviesQuery.OrderBy(m => m.Title),
                "releaseDate" => moviesQuery.OrderBy(m => m.ReleaseDate),
                "rating" => moviesQuery.OrderBy(m => m.Rating),
                _ => moviesQuery
            };

            var movies = await moviesQuery.ToListAsync();

            return new JsonResult(movies);
        }
    }
}
