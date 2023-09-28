namespace LinkShorteningApp.Services.ShortLinkService;

public interface IShortLinkService
{
    string GenerateShortLink(string url);
    Task<bool> CreateShortLink(string url, string shortUrl, CancellationToken cancellationToken);
    Task<string?> GetRedirectUrl(string shortUrl, CancellationToken cancellationToken);
}
