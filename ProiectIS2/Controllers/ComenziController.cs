using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectIS2.Data;
using ProiectIS2.Models;

namespace ProiectIS2.Controllers
{
    public class ComenziController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComenziController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTA COMENZILOR: Vedem tot ce s-a comandat
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comenzi.Include(c => c.Produs);
            return View(await applicationDbContext.ToListAsync());
        }

        // PAGINA DE COMANDĂ (Clientul alege produsul)
        public IActionResult Create()
        {
            // Dacă nu avem produse, trimitem o listă goală să nu crape
            var listaProd = _context.Produse.ToList() ?? new List<Produs>();
            ViewBag.ListaProduse = new SelectList(listaProd, "Id", "Name");
            return View();
        }

        // PROCESUL AUTOMAT DE SALVARE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int ProdusId)
        {
            //Căutăm produsul selectat pentru a-i afla prețul (sau punem un preț default dacă modelul nu are câmpul Pret)
            var produs = await _context.Produse.FindAsync(ProdusId);
            
            if (produs == null)
            {
                ModelState.AddModelError("", "Produsul selectat nu există.");
                ViewBag.ListaProduse = new SelectList(_context.Produse.ToList(), "Id", "Name");
                return View();
            }

            // CREĂM COMANDA AUTOMAT
            var comandaNoua = new Comanda
            {
                ProdusId = ProdusId,
                DataPlasarii = DateTime.Now, // Automat data de acum
                NumarOrdine = new Random().Next(100, 999), // Automat număr random de ordine
                EstePlatita = false, // Implicit nu e plătită
                
                // Dacă modelul tău Produs are câmpul Pret, folosim produs.Pret
                // Dacă nu îl are încă, punem un preț simbolic de 30 RON pentru simulare
                Total = 30 
            };

            _context.Add(comandaNoua);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Restul metodelor (Edit, Delete) pot rămâne simple
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var comanda = await _context.Comenzi.Include(c => c.Produs).FirstOrDefaultAsync(m => m.Id == id);
            return View(comanda);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comanda = await _context.Comenzi.FindAsync(id);
            if (comanda != null) _context.Comenzi.Remove(comanda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}