using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace MovieCatalog.Pages
{
    public class LocalizationDebugModel : PageModel
    {
        private readonly IStringLocalizer<Resources.SharedResource> _localizer;

        public LocalizationDebugModel(IStringLocalizer<Resources.SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string CurrentCulture { get; set; }
        public string MovieCatalog { get; set; }
        public string Home { get; set; }
        public string Movies { get; set; }
        public string Privacy { get; set; }

        public void OnGet()
        {
            CurrentCulture = CultureInfo.CurrentCulture.Name;
            MovieCatalog = _localizer["MovieCatalog"];
            Home = _localizer["Home"];
            Movies = _localizer["Movies"];
            Privacy = _localizer["Privacy"];
        }
    }
}
