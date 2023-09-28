namespace LinkShorteningApp.Services.DateTimeService;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
