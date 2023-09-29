using LinkShorteningApp.Database;
using LinkShorteningApp.Repositories;
using LinkShorteningApp.Services.DateTimeService;
using LinkShorteningApp.Services.ShortLinkService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
}, ServiceLifetime.Scoped);

builder.Services.AddTransient<IDateTime, DateTimeService>();
builder.Services.AddScoped<IShortLinkService, ShortLinkService>();
builder.Services.AddScoped<IUrlRepository, UrlRepository>();

var app = builder.Build();

app.Services.MigrateDatabase(app.Configuration);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "ShortLinkRoute",
        pattern: "{shortUrl}",
        defaults: new { controller = "Url", action = "RedirectToOriginalUrl" });
});

app.MapRazorPages();

app.Run();
