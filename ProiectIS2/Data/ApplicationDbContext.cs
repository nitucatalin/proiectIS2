using ProiectIS2.Models;
using Microsoft.EntityFrameworkCore;
namespace ProiectIS2.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    //Crearea tabelelor in data base ul nostru
    public DbSet<Restaurant> Restaurante { get; set; }
    public DbSet<Produs> Produse { get; set; }
    public DbSet<Comanda> Comenzi { get; set; }
}