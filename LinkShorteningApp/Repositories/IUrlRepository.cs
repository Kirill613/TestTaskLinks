using LinkShorteningApp.Models;

namespace LinkShorteningApp.Repositories;

public interface IUrlRepository
{
    Task<List<UrlInfoModel>> GetAllUrls();
    Task<UrlInfoModel> Get(Guid id);

    void Add(string url, string shortUrl);
    void Update(UrlInfoModel model);
    Task Delete(Guid id);

    Task SaveChangesAsync();
}
