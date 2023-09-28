using LinkShorteningApp.Models;
using LinkShorteningApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkShorteningApp.Pages.Url
{
    public class IndexModel : PageModel
    {
        private readonly IUrlRepository _urlRepository;

        [BindProperty]
        public IEnumerable<UrlInfoModel> UrlInfos { get; set; }

        public IndexModel(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            UrlInfos = await _urlRepository.GetAllUrls();

            ViewData["BaseUrl"] = $"{Request.Scheme}://{Request.Host}/"; 

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        { 
            await _urlRepository.Delete(id);
            await _urlRepository.SaveChangesAsync();

            UrlInfos = await _urlRepository.GetAllUrls();

            return Page();
        }
    }
}
