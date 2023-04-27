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
    public class ShoppingCandiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCandiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingCandies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShoppingCandies.Include(s => s.Candies).Include(s => s.Customers);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShoppingCandies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShoppingCandies == null)
            {
                return NotFound();
            }

            var shoppingCandy = await _context.ShoppingCandies
                .Include(s => s.Candies)
                .Include(s => s.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCandy == null)
            {
                return NotFound();
            }

            return View(shoppingCandy);
        }

        // GET: ShoppingCandies/Create
        public IActionResult Create()
        {
            ViewData["CandiesId"] = new SelectList(_context.Cadies, "Id", "Id");
            ViewData["CustomersId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ShoppingCandies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CandiesId,CustomersId,Size,RegisterOn")] ShoppingCandy shoppingCandy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCandy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CandiesId"] = new SelectList(_context.Cadies, "Id", "Id", shoppingCandy.CandiesId);
            ViewData["CustomersId"] = new SelectList(_context.Users, "Id", "Id", shoppingCandy.CustomersId);
            return View(shoppingCandy);
        }

        // GET: ShoppingCandies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShoppingCandies == null)
            {
                return NotFound();
            }

            var shoppingCandy = await _context.ShoppingCandies.FindAsync(id);
            if (shoppingCandy == null)
            {
                return NotFound();
            }
            ViewData["CandiesId"] = new SelectList(_context.Cadies, "Id", "Id", shoppingCandy.CandiesId);
            ViewData["CustomersId"] = new SelectList(_context.Users, "Id", "Id", shoppingCandy.CustomersId);
            return View(shoppingCandy);
        }

        // POST: ShoppingCandies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CandiesId,CustomersId,Size,RegisterOn")] ShoppingCandy shoppingCandy)
        {
            if (id != shoppingCandy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCandy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCandyExists(shoppingCandy.Id))
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
            ViewData["CandiesId"] = new SelectList(_context.Cadies, "Id", "Id", shoppingCandy.CandiesId);
            ViewData["CustomersId"] = new SelectList(_context.Users, "Id", "Id", shoppingCandy.CustomersId);
            return View(shoppingCandy);
        }

        // GET: ShoppingCandies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShoppingCandies == null)
            {
                return NotFound();
            }

            var shoppingCandy = await _context.ShoppingCandies
                .Include(s => s.Candies)
                .Include(s => s.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCandy == null)
            {
                return NotFound();
            }

            return View(shoppingCandy);
        }

        // POST: ShoppingCandies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShoppingCandies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ShoppingCandies'  is null.");
            }
            var shoppingCandy = await _context.ShoppingCandies.FindAsync(id);
            if (shoppingCandy != null)
            {
                _context.ShoppingCandies.Remove(shoppingCandy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCandyExists(int id)
        {
          return _context.ShoppingCandies.Any(e => e.Id == id);
        }
    }
}
