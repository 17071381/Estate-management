using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Estate_management.Data;
using Estate_management.Models;

namespace Estate_management.Controllers
{
    public class EstatesController : Controller
    {
        private readonly LandDbContext _context;

        public EstatesController(LandDbContext context)
        {
            _context = context;
        }

        // GET: Estates
        public async Task<IActionResult> Index()
        {
              return _context.Estates != null ? 
                          View(await _context.Estates.ToListAsync()) :
                          Problem("Entity set 'LandDbContext.Estates'  is null.");
        }

        // GET: Estates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estates == null)
            {
                return NotFound();
            }

            var estate = await _context.Estates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estate == null)
            {
                return NotFound();
            }

            return View(estate);
        }

        // GET: Estates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageUrl,Type,descripion,Price")] Estate estate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estate);
        }

        // GET: Estates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estates == null)
            {
                return NotFound();
            }

            var estate = await _context.Estates.FindAsync(id);
            if (estate == null)
            {
                return NotFound();
            }
            return View(estate);
        }

        // POST: Estates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,descripion,Price")] Estate estate)
        {
            if (id != estate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstateExists(estate.Id))
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
            return View(estate);
        }

        // GET: Estates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estates == null)
            {
                return NotFound();
            }

            var estate = await _context.Estates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estate == null)
            {
                return NotFound();
            }

            return View(estate);
        }

        // POST: Estates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estates == null)
            {
                return Problem("Entity set 'LandDbContext.Estates'  is null.");
            }
            var estate = await _context.Estates.FindAsync(id);
            if (estate != null)
            {
                _context.Estates.Remove(estate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstateExists(int id)
        {
          return (_context.Estates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
