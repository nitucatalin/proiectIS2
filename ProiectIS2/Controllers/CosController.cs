using Microsoft.AspNetCore.Mvc;
using ProiectIS2.Data;
using ProiectIS2.Models;
using System.Text.Json;

namespace ProiectIS2.Controllers
{
    public class CosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CosController(ApplicationDbContext context) => _context = context;

        public IActionResult AdaugaInCos(int id)
        {
            var produs = _context.Produse.Find(id);
            if (produs == null) return NotFound();

            var cos = ObtineCosDinSesiune();
            cos.Add(produs);
            SalveazaCosInSesiune(cos);
            
            TempData["SuccesCos"] = $"✅ Produsul '{produs.Nume}' a fost adăugat în coș!";

            return RedirectToAction("Index", "Produse");
        }

        public IActionResult Index()
        {
            var cos = ObtineCosDinSesiune();
            ViewBag.Total = cos.Sum(p => p.Pret);
            return View(cos);
        }

        public IActionResult Checkout()
        {
            var cos = ObtineCosDinSesiune();
            if (!cos.Any()) return RedirectToAction("Index", "Produse");
            
            ViewBag.Total = cos.Sum(p => p.Pret);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Finalizeaza(string metodaPlata)
        {
            var cos = ObtineCosDinSesiune();
            if (!cos.Any()) return RedirectToAction("Index");

            foreach (var produs in cos)
            {
                var comanda = new Comanda
                {
                    ProdusId = produs.Id,
                    DataPlasarii = DateTime.Now,
                    Total = produs.Pret,
                    NumarOrdine = new Random().Next(100, 999),
                    EstePlatita = (metodaPlata == "Card")
                };
                _context.Comenzi.Add(comanda);
            }

            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("CosCumparaturi");

            return RedirectToAction("Index", "Comenzi");
        }

        private List<Produs> ObtineCosDinSesiune()
        {
            var date = HttpContext.Session.GetString("CosCumparaturi");
            return date == null ? new List<Produs>() : JsonSerializer.Deserialize<List<Produs>>(date);
        }

        private void SalveazaCosInSesiune(List<Produs> cos)
        {
            HttpContext.Session.SetString("CosCumparaturi", JsonSerializer.Serialize(cos));
        }
    }
}