using FarmTrack.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Idk hur jag fick detta att funka men rör inget :)
var optionsBuilder = new DbContextOptionsBuilder<FarmContext>();
optionsBuilder.UseSqlServer("Server=(localdb)\\FarmTrackDB;Database=FarmTrackDB;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=SSPI"); //Create options for dbcontext
FarmContext _dbContext = new FarmContext(optionsBuilder.Options);

_dbContext.Database.EnsureCreated(); //Make sure that the database is created, create it if it does not exist

SqlConnection conn = new SqlConnection(); //Connect to the database
conn.ConnectionString =
  "Data Source=(localdb)\\FarmTrackDB;" +
  "Initial Catalog=FarmTrackDB;" +
  "Integrated Security=SSPI;";
conn.Open(); //Open the connection



//conn.ConnectionString = "Server=(localdb)\\FarmTrackDB;Database=FarmTrackDB;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=True";
builder.Services.AddDbContext<FarmContext>(options =>
{
    options.UseSqlServer(conn);//Use the connectionstring
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
