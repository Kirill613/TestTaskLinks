using LinkShorteningApp.Database;
using LinkShorteningApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShorteningApp.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly ApplicationDbContext _context;

    public UrlRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UrlInfoModel> Get(Guid id)
    {
        return await _context.UrlInfos
            .Where(x => !x.IsDeleted)
            .SingleAsync(x => x.Id == id);
    }

    public async Task<List<UrlInfoModel>> GetAllUrls()
    {
        return await _context.UrlInfos
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public void Add(string url, string shortUrl)
    {
        _context.UrlInfos.Add(
            new UrlInfoModel
            {
                ShortUrl = shortUrl,
                Url = url,
            }
        );
    }

    public void Update(UrlInfoModel model)
    {
        _context.UrlInfos.Update(model);
    }

    public async Task Delete(Guid id)
    {
        var result = await _context.UrlInfos.SingleAsync(x => x.Id == id);

        result.IsDeleted = true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
