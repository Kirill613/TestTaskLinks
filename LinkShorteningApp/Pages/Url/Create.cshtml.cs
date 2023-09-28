using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LinkShorteningApp.Services.ShortLinkService;
using LinkShorteningApp.Validation;

namespace LinkShorteningApp.Pages.Url
{
    public class CreateModel : PageModel
    {
        private readonly IShortLinkService _shortLinkService;

        [BindProperty]
        [UrlValidation(ErrorMessage = "¬ведите корректный Url.")]
        public string FullUrl { get; set; }

        public CreateModel(IShortLinkService shortLinkService)
        {
            _shortLinkService = shortLinkService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var shortUrl = _shortLinkService.GenerateShortLink(FullUrl);

            var result = await _shortLinkService.CreateShortLink(FullUrl, shortUrl, cancellationToken);

            if (!result)
            {
                ModelState.AddModelError("FullUrl", "Ётот Url уже существует в системе.");

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}

