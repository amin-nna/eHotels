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
    public class CentralOfficeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CentralOfficeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CentralOffice
        public async Task<IActionResult> Index()
        {
            return _context.Hotel != null ?
                         View(await _context.CentralOffice.ToListAsync()) :
                         Problem("Entity set 'ApplicationDbContext.Hotel'  is null.");
        }

        public async Task<IActionResult> IndexChain(string name)
        {
            var centralOffices = await _context.CentralOffice
                .Where(co => co.HotelChain_Name == name)
                .ToListAsync();

            return PartialView(centralOffices);
        }

        // GET: CentralOffice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CentralOffice == null)
            {
                return NotFound();
            }

            var centralOffices = await _context.CentralOffice
                .Include(c => c.HotelChain)
                .FirstOrDefaultAsync(m => m.Office_ID == id);
            if (centralOffices == null)
            {
                return NotFound();
            }

            return View(centralOffices);
        }

        // GET: CentralOffice/Create
        public IActionResult Create()
        {
            ViewBag.HotelChainList = _context.HotelChain.Select(h => h.Name).ToList();
            return View();
        }

        // POST: CentralOffice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Office_ID,HotelChain_Name,Street,City,Province,PostalCode")] CentralOffices centralOffices)
        {
            ModelState.Remove("HotelChain");
            
            if (ModelState.IsValid)
            {
                _context.Add(centralOffices);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Administrator");
            }
            ViewBag.HotelChainList = _context.HotelChain.Select(h => h.Name).ToList();
            return View(centralOffices);
        }

        // GET: CentralOffice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CentralOffice == null)
            {
                return NotFound();
            }

            var centralOffices = await _context.CentralOffice.FindAsync(id);
            if (centralOffices == null)
            {
                return NotFound();
            }
            ViewData["HotelChain_Name"] = new SelectList(_context.HotelChain, "Name", "Name", centralOffices.HotelChain_Name);
            return View(centralOffices);
        }

        // POST: CentralOffice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Office_ID,HotelChain_Name,Street,City,Province,PostalCode")] CentralOffices centralOffices)
        {
            if (id != centralOffices.Office_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centralOffices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentralOfficesExists(centralOffices.Office_ID))
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
            ViewData["HotelChain_Name"] = new SelectList(_context.HotelChain, "Name", "Name", centralOffices.HotelChain_Name);
            return View(centralOffices);
        }

        // GET: CentralOffice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CentralOffice == null)
            {
                return NotFound();
            }

            var centralOffices = await _context.CentralOffice
                .Include(c => c.HotelChain)
                .FirstOrDefaultAsync(m => m.Office_ID == id);
            if (centralOffices == null)
            {
                return NotFound();
            }

            return View(centralOffices);
        }

        // POST: CentralOffice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CentralOffice == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CentralOffice'  is null.");
            }
            var centralOffices = await _context.CentralOffice.FindAsync(id);
            if (centralOffices != null)
            {
                _context.CentralOffice.Remove(centralOffices);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentralOfficesExists(int id)
        {
          return (_context.CentralOffice?.Any(e => e.Office_ID == id)).GetValueOrDefault();
        }
    }
}
