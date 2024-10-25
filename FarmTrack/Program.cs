using FarmTrack.Models; 
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Lägg till DbContext-konfiguration för att hantera databasanvändning
builder.Services.AddDbContext<FarmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Add cookie policy globally
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.Secure = CookieSecurePolicy.SameAsRequest; // Tillåt både HTTP och HTTPS
});

var app = builder.Build();


// Se till att databasen skapas om den inte redan finns
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FarmContext>();
    dbContext.Database.EnsureCreated(); // Skapa databasen om den inte finns
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
