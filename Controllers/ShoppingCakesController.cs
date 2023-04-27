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
    public class ShoppingCakesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCakesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingCakes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShoppingCakes.Include(s => s.Cakes).Include(s => s.Customers);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShoppingCakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShoppingCakes == null)
            {
                return NotFound();
            }

            var shoppingCake = await _context.ShoppingCakes
                .Include(s => s.Cakes)
                .Include(s => s.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCake == null)
            {
                return NotFound();
            }

            return View(shoppingCake);
        }

        // GET: ShoppingCakes/Create
        public IActionResult Create()
        {
            ViewData["CakeId"] = new SelectList(_context.Cakes, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ShoppingCakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CakeId,CustomerId,Quality,Size,Requirement,RegisterOn")] ShoppingCake shoppingCake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CakeId"] = new SelectList(_context.Cakes, "Id", "Id", shoppingCake.CakeId);
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id", shoppingCake.CustomerId);
            return View(shoppingCake);
        }

        // GET: ShoppingCakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShoppingCakes == null)
            {
                return NotFound();
            }

            var shoppingCake = await _context.ShoppingCakes.FindAsync(id);
            if (shoppingCake == null)
            {
                return NotFound();
            }
            ViewData["CakeId"] = new SelectList(_context.Cakes, "Id", "Id", shoppingCake.CakeId);
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id", shoppingCake.CustomerId);
            return View(shoppingCake);
        }

        // POST: ShoppingCakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CakeId,CustomerId,Quality,Size,Requirement,RegisterOn")] ShoppingCake shoppingCake)
        {
            if (id != shoppingCake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCakeExists(shoppingCake.Id))
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
            ViewData["CakeId"] = new SelectList(_context.Cakes, "Id", "Id", shoppingCake.CakeId);
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id", shoppingCake.CustomerId);
            return View(shoppingCake);
        }

        // GET: ShoppingCakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShoppingCakes == null)
            {
                return NotFound();
            }

            var shoppingCake = await _context.ShoppingCakes
                .Include(s => s.Cakes)
                .Include(s => s.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCake == null)
            {
                return NotFound();
            }

            return View(shoppingCake);
        }

        // POST: ShoppingCakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShoppingCakes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ShoppingCakes'  is null.");
            }
            var shoppingCake = await _context.ShoppingCakes.FindAsync(id);
            if (shoppingCake != null)
            {
                _context.ShoppingCakes.Remove(shoppingCake);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCakeExists(int id)
        {
          return _context.ShoppingCakes.Any(e => e.Id == id);
        }
    }
}
