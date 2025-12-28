using System;
using System.Collections.Generic;
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

        // GET: Comenzi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comenzi.ToListAsync());
        }

        // GET: Comenzi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.Comenzi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        // GET: Comenzi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comenzi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataPlasarii,Total,NumarOrdine,EstePlatita")] Comanda comanda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comanda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comanda);
        }

        // GET: Comenzi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.Comenzi.FindAsync(id);
            if (comanda == null)
            {
                return NotFound();
            }
            return View(comanda);
        }

        // POST: Comenzi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataPlasarii,Total,NumarOrdine,EstePlatita")] Comanda comanda)
        {
            if (id != comanda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comanda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComandaExists(comanda.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comanda);
        }

        // GET: Comenzi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.Comenzi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        // POST: Comenzi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comanda = await _context.Comenzi.FindAsync(id);
            if (comanda != null)
            {
                _context.Comenzi.Remove(comanda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComandaExists(int id)
        {
            return _context.Comenzi.Any(e => e.Id == id);
        }
    }
}
