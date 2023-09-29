using Microsoft.EntityFrameworkCore;

namespace LinkShorteningApp.Database;

public static class MigrationManager
{
    public static void MigrateDatabase(this IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        try
        {
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}