using Microsoft.EntityFrameworkCore;
using ProiectIS2.Models;

namespace ProiectIS2.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                       serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Restaurante.Any())
                {
                    return; 
                }

                // Creăm restaurantele doar cu proprietățile pe care le ai (Nume)
                var r1 = new Restaurant { Name = "Shaormeria NVM" };
                var r2 = new Restaurant { Name = "Pasta Delight" };
                var r3 = new Restaurant { Name = "Sushi Star" };

                context.Restaurante.AddRange(r1, r2, r3);
                context.SaveChanges(); 

                context.Produse.AddRange(
                    new Produs { Nume = "Lipie mare Vita", Pret = 37, RestaurantId = r1.Id },
                    new Produs { Nume = "Lipie mica Vita", Pret = 32, RestaurantId = r1.Id },
                    new Produs { Nume = "Lipie mare Pui", Pret = 28, RestaurantId = r1.Id },
                    new Produs { Nume = "Lipie mica Pui", Pret = 25, RestaurantId = r1.Id },
                    new Produs { Nume = "Paste Carbonara", Pret = 42, RestaurantId = r2.Id },
                    new Produs { Nume = "Paste Bolognese", Pret = 38, RestaurantId = r2.Id },
                    new Produs { Nume = "Lasagna", Pret = 45, RestaurantId = r2.Id },
                    new Produs { Nume = "Sushi Combo", Pret = 65, RestaurantId = r3.Id }
                );

                context.SaveChanges();
            }
        }
    }
}