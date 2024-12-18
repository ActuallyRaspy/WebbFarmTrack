using FarmTrack.Models; 
using Microsoft.EntityFrameworkCore;
using FarmTrack.Views.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<EnsureLoggedInAttribute>(); // To keep non-logged in users out of pages they shouldnt see.
});

// L�gg till DbContext-konfiguration f�r att hantera databasanv�ndning
builder.Services.AddDbContext<FarmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(300);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add cookie policy globally
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.Secure = CookieSecurePolicy.SameAsRequest; // Till�t b�de HTTP och HTTPS
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

app.UseSession();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();