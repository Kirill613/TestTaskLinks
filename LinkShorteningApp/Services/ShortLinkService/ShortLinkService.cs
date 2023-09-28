using LinkShorteningApp.Database;
using LinkShorteningApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LinkShorteningApp.Services.ShortLinkService;

public class ShortLinkService : IShortLinkService
{
    private readonly IUrlRepository _urlRepository;
    private readonly ApplicationDbContext _context;
    private readonly static string _secretSalt = "secret salt";

    public ShortLinkService(IUrlRepository urlRepository, ApplicationDbContext context)
    {
        _urlRepository = urlRepository;     
        _context = context;
    }

    public string GenerateShortLink(string url)
    {
        using var hasher = SHA256.Create();

        byte[] hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(url + _secretSalt));

        string hash = BitConverter
          .ToString(hashBytes)
          .Replace("-", string.Empty);

        string shortHashCode = hash[..8];

        return shortHashCode;
    }

    public async Task<bool> CreateShortLink(string url, string shortUrl, CancellationToken cancellationToken)
    {
        using var transaction = await _context.BeginTransactionAsync(cancellationToken);

        var isExist = await _context.UrlInfos
            .AnyAsync(x => !x.IsDeleted && (x.Url == url || x.ShortUrl == shortUrl), cancellationToken);

        if (isExist) return false;

        _urlRepository.Add(url, shortUrl);

        await _context.SaveChangesAsync(cancellationToken);

        transaction.Commit();

        return true;
    }

    public async Task<string?> GetRedirectUrl(string shortUrl, CancellationToken cancellationToken)
    {
        using var transaction = await _context.BeginTransactionAsync(cancellationToken);

        var urlInfo = await _context.UrlInfos
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(l => l.ShortUrl == shortUrl, cancellationToken);

        if (urlInfo != null)
        {
            urlInfo.ClicksCount++;
            await _context.SaveChangesAsync(cancellationToken);
        }

        transaction.Commit();

        return urlInfo?.Url;
    }
}
