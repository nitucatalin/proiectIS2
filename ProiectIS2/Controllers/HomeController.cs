using Microsoft.AspNetCore.Mvc;
using ProiectIS2.Data;
using System.Linq;

namespace ProiectIS2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Calculăm statistici reale din baza de date
            ViewBag.TotalComenzi = _context.Comenzi.Count();
            ViewBag.VenitTotal = _context.Comenzi.Sum(c => c.Total);
            ViewBag.NrRestaurante = _context.Restaurante.Count();
            ViewBag.NrProduse = _context.Produse.Count();

            return View();
        }
        // PAGINA DESPRE NOI
        public IActionResult About()
        {
            return View();
        }
    }
}