using Microsoft.EntityFrameworkCore;
using ProiectIS2.Data;

var builder = WebApplication.CreateBuilder(args);


// SERVICII 

builder.Services.AddControllersWithViews()
    
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// Adăugăm contextul bazei de date
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurare SESIUNE 
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
// Configurare baza de date >> DOCKER + SEED
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        // Apelăm clasa SeedData care face și EnsureCreated() și adaugă datele
        SeedData.Initialize(services);
        Console.WriteLine("------> Baza de date a fost verificata/populata cu succes!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("------> EROARE la popularea bazei de date: " + ex.Message);
    }
}
//MIDDLEWARE

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// ACTIVARE SESIUNE
app.UseSession(); 

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();