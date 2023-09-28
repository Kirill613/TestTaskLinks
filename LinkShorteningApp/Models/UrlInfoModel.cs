using LinkShorteningApp.Models.Common;

namespace LinkShorteningApp.Models;

public class UrlInfoModel : AuditableModel, ISoftDelete
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string ShortUrl { get; set; }
    public int ClicksCount { get; set; } = 0;

    public bool IsDeleted { get; set; } = false;
}
