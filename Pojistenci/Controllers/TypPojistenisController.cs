using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pojistenci.Data;
using Pojistenci.Models;

namespace Pojistenci.Controllers
{
    public class TypPojistenisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypPojistenisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypPojistenis
        public async Task<IActionResult> Index()
        {
              return _context.TypPojisteni != null ? 
                          View(await _context.TypPojisteni.ToListAsync()) :
                          Problem("Entity set 'PojistenciContext.TypPojisteni'  is null.");
        }

        // GET: TypPojistenis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypPojisteni == null)
            {
                return NotFound();
            }

            var typPojisteni = await _context.TypPojisteni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typPojisteni == null)
            {
                return NotFound();
            }

            return View(typPojisteni);
        }

        // GET: TypPojistenis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypPojistenis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazev")] TypPojisteni typPojisteni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typPojisteni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typPojisteni);
        }

        // GET: TypPojistenis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypPojisteni == null)
            {
                return NotFound();
            }

            var typPojisteni = await _context.TypPojisteni.FindAsync(id);
            if (typPojisteni == null)
            {
                return NotFound();
            }
            return View(typPojisteni);
        }

        // POST: TypPojistenis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazev")] TypPojisteni typPojisteni)
        {
            if (id != typPojisteni.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typPojisteni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypPojisteniExists(typPojisteni.Id))
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
            return View(typPojisteni);
        }

        // GET: TypPojistenis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypPojisteni == null)
            {
                return NotFound();
            }

            var typPojisteni = await _context.TypPojisteni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typPojisteni == null)
            {
                return NotFound();
            }

            return View(typPojisteni);
        }

        // POST: TypPojistenis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypPojisteni == null)
            {
                return Problem("Entity set 'PojistenciContext.TypPojisteni'  is null.");
            }
            var typPojisteni = await _context.TypPojisteni.FindAsync(id);
            if (typPojisteni != null)
            {
                _context.TypPojisteni.Remove(typPojisteni);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypPojisteniExists(int id)
        {
          return (_context.TypPojisteni?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
