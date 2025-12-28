namespace ProiectIS2.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    //Lista de produse care apartin restaurantului
    public List<Produs> Produse { get; set; } = new();
}