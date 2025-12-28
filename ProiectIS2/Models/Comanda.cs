namespace ProiectIS2.Models;

public class Comanda
{
    public int Id { get; set; }
    public DateTime DataPlasarii { get; set; } = DateTime.Now;
    public decimal Total { get; set; }
    public string NumarOrdine { get; set; } = string.Empty;
    public bool EstePlatita { get; set; } = false;
    
}