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
    public class PojistenecsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PojistenecsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pojistenecs
        public async Task<IActionResult> Index()
        {
            IEnumerable<Pojistenec>? objList = _context.Pojistenec;

            foreach(var obj in objList)
            {
                obj.TypPojisteni = _context.TypPojisteni.FirstOrDefault(u => u.Id == obj.TypPojisteniId);
            }

            return View(objList);
        }
        // POST: Pojistenci
        [HttpPost]
        public async Task<IActionResult> Index(string searchPrijmeni)
        {
            if(!String.IsNullOrEmpty(searchPrijmeni))
            {
                return _context.Pojistenec != null ?
                          View("Index",await _context.Pojistenec.Where(x => x.Surname!.Contains(searchPrijmeni)).ToListAsync()) :
                          View("Index");
            }
            return RedirectToAction("Index");
        }
        // POST: Pojistenci
        [HttpPost]
        public async Task<IActionResult> Index3(string searchKraj)
        {
            if(!String.IsNullOrEmpty(searchKraj))
            {
                return _context.Pojistenec != null ?
                          View("Index",await _context.Pojistenec.Where(x => x.Kraj!.Contains(searchKraj)).ToListAsync()) :
                          View("Index");
            }
            return RedirectToAction("Index");
        }
        public int Prevod(string text)
        {
            int poradi = 0;
            switch(text)
            {
                case "úrazové pojištění": poradi = 1; break;
                case "důchodové pojištění": poradi = 2; break;
                case "nemocenské pojištění": poradi = 3; break;
                case "cestovní pojištění": poradi = 4; break;
                case "havarijní pojištění": poradi = 5; break;
                case "pojištění proti odcizení": poradi = 6; break;
                case "živelní pojištění": poradi = 7; break;
                case "pojištění vozidel": poradi = 8; break;
                case "pojištění podnikání": poradi = 9; break;
                case "pojištění osob": poradi = 10; break;
            }
            return poradi;
        }
        // GET: Pojistenci
        public async Task<IActionResult> Index2(string TypPojisteni)
        {
            // Use LINQ to get list of genres.
            //IQueryable<string> genreQuery = from m in _context.Movie
            //                                orderby m.Genre
            //                                select m.Genre;
            var typyPojisteni = from g in _context.TypPojisteni
                                orderby g.Nazev
                                select g.Nazev;

            var pojistenci = from m in _context.Pojistenec
                         select m;

            int poradi = Prevod(TypPojisteni);
            if(!string.IsNullOrEmpty(TypPojisteni))
            {
                
                pojistenci = pojistenci.Where(x => x.TypPojisteniId == poradi);
            }

            var ppwm = new PojistenecPojisteniViewModel
            {
                TypyPojisteni = new SelectList(await typyPojisteni.Distinct().ToListAsync()),
                Pojistenci = await pojistenci.ToListAsync()
            };

            return View(ppwm);
        }

        // GET: Pojistenecs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pojistenec == null)
            {
                return NotFound();
            }

            var pojistenec = await _context.Pojistenec.FindAsync(id);
            if (pojistenec == null)
            {
                return NotFound();
            }

            return View(pojistenec);
        }

        // GET: Pojistenecs/Create
        public IActionResult Create()
        {
            PojistenecViewModel pVM = new PojistenecViewModel()
            {
                Pojistenec = new Pojistenec(),
                TypeDropDownPojisteni = _context.TypPojisteni.Select(i => new SelectListItem
                {
                    Text = i.Nazev,
                    Value = i.Id.ToString()
                })
            };

            return View(pVM);
        }

        // POST: Pojistenecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PojistenecViewModel obj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obj.Pojistenec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        // GET: Pojistenecs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pojistenec == null)
            {
                return NotFound();
            }

            var pojistenec = await _context.Pojistenec.FindAsync(id);
            if (pojistenec == null)
            {
                return NotFound();
            }
            return View(pojistenec);
        }

        // POST: Pojistenecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Surname,Vek,Registrovan,Kraj,TypPojisteniId")] Pojistenec pojistenec)
        {
            if (id != pojistenec.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pojistenec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PojistenecExists(pojistenec.Id))
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
            return View(pojistenec);
        }

        // GET: Pojistenecs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pojistenec == null)
            {
                return NotFound();
            }

            var pojistenec = await _context.Pojistenec
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pojistenec == null)
            {
                return NotFound();
            }

            return View(pojistenec);
        }

        // POST: Pojistenecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pojistenec == null)
            {
                return Problem("Entity set 'PojistenciContext.Pojistenec'  is null.");
            }
            var pojistenec = await _context.Pojistenec.FindAsync(id);
            if (pojistenec != null)
            {
                _context.Pojistenec.Remove(pojistenec);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PojistenecExists(int id)
        {
          return (_context.Pojistenec?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
