using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eHotels.Areas.Identity.Data;
using eHotels.Models;

namespace eHotels.Controllers
{
    public class HotelChainController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelChainController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HotelChain
        public async Task<IActionResult> Index()
        {
              return _context.HotelChain != null ? 
                          View(await _context.HotelChain.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.HotelChain'  is null.");
        }

        // GET: HotelChain/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.HotelChain == null)
            {
                return NotFound();
            }

            var hotelChains = await _context.HotelChain
                .FirstOrDefaultAsync(m => m.Name == id);
            if (hotelChains == null)
            {
                return NotFound();
            }

            return View(hotelChains);
        }

        // GET: HotelChain/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HotelChain/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Street,City,Province,PostalCode,Hotels,Rating")] HotelChains hotelChains)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelChains);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotelChains);
        }

        // GET: HotelChain/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.HotelChain == null)
            {
                return NotFound();
            }

            var hotelChains = await _context.HotelChain.FindAsync(id);
            if (hotelChains == null)
            {
                return NotFound();
            }
            return View(hotelChains);
        }

        // POST: HotelChain/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Street,City,Province,PostalCode,Hotels,Rating")] HotelChains hotelChains)
        {
            if (id != hotelChains.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelChains);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelChainsExists(hotelChains.Name))
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
            return View(hotelChains);
        }

        // GET: HotelChain/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.HotelChain == null)
            {
                return NotFound();
            }

            var hotelChains = await _context.HotelChain
                .FirstOrDefaultAsync(m => m.Name == id);
            if (hotelChains == null)
            {
                return NotFound();
            }

            return View(hotelChains);
        }

        // POST: HotelChain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.HotelChain == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HotelChain'  is null.");
            }
            var hotelChains = await _context.HotelChain.FindAsync(id);
            if (hotelChains != null)
            {
                _context.HotelChain.Remove(hotelChains);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelChainsExists(string id)
        {
          return (_context.HotelChain?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
