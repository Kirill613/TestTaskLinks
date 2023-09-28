using LinkShorteningApp.Services.ShortLinkService;
using Microsoft.AspNetCore.Mvc;

namespace LinkShorteningApp.Controllers;

public class UrlController : ControllerBase
{
    private readonly IShortLinkService _shortLinkService;

    public UrlController(IShortLinkService shortLinkService)
    {
        _shortLinkService = shortLinkService;
    }

    [HttpGet]
    public async Task<IActionResult> RedirectToOriginalUrl(string shortUrl, CancellationToken cancellationToken)
    {
        var url = await _shortLinkService.GetRedirectUrl(shortUrl, cancellationToken);

        if (url == null)
        {
            return NotFound();
        }

        return new RedirectResult(url);
    }
}
