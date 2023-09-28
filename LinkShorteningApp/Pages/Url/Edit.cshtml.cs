using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LinkShorteningApp.Models;
using LinkShorteningApp.Repositories;

namespace LinkShorteningApp.Pages.Url
{
    public class EditModel : PageModel
    {
        private readonly IUrlRepository _urlRepository;

        [BindProperty]
        public UrlInfoModel UrlInfo { get; set; }

        public EditModel(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToPage("Index");
            }

            UrlInfo = await _urlRepository.Get(id);
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }        
  
            _urlRepository.Update(UrlInfo);

            await _urlRepository.SaveChangesAsync();

            return Page();
        }
    }
}
