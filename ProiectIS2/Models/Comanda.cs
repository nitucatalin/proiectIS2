using System;
using System.ComponentModel.DataAnnotations;

namespace ProiectIS2.Models
{
    public class Comanda
    {
        public int Id { get; set; }

        [Display(Name = "Data Plasării")]
        public DateTime DataPlasarii { get; set; }

        public decimal Total { get; set; }

        [Display(Name = "Număr Ordine")]
        public int NumarOrdine { get; set; }

        public bool EstePlatita { get; set; }
        
        [Display(Name = "Produs")]
        public int ProdusId { get; set; } // Cheia externă
        public Produs? Produs { get; set; } // Proprietatea de navigare
    }
}