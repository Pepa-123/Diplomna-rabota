using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class CandiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Candies
        public async Task<IActionResult> Index()
        {
              return View(await _context.Cadies.ToListAsync());
        }

        // GET: Candies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cadies == null)
            {
                return NotFound();
            }

            var candy = await _context.Cadies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candy == null)
            {
                return NotFound();
            }

            return View(candy);
        }

        // GET: Candies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Size,ImageURL,Price,RegisterOn")] Candy candy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candy);
        }

        // GET: Candies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cadies == null)
            {
                return NotFound();
            }

            var candy = await _context.Cadies.FindAsync(id);
            if (candy == null)
            {
                return NotFound();
            }
            return View(candy);
        }

        // POST: Candies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Size,ImageURL,Price,RegisterOn")] Candy candy)
        {
            if (id != candy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandyExists(candy.Id))
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
            return View(candy);
        }

        // GET: Candies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cadies == null)
            {
                return NotFound();
            }

            var candy = await _context.Cadies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candy == null)
            {
                return NotFound();
            }

            return View(candy);
        }

        // POST: Candies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cadies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cadies'  is null.");
            }
            var candy = await _context.Cadies.FindAsync(id);
            if (candy != null)
            {
                _context.Cadies.Remove(candy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandyExists(int id)
        {
          return _context.Cadies.Any(e => e.Id == id);
        }
    }
}
