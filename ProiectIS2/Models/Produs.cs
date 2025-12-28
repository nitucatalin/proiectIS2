namespace ProiectIS2.Models;

public class Produs
{
    public int Id { get; set; }
    public string Nume { get; set; } = string.Empty;
    public decimal Pret { get; set; }
    
    //Legatura cu Restaurant.cs
    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}