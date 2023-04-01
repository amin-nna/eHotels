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
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hotel
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hotel.Include(h => h.HotelChain);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexSearchFilter()
        {
            var applicationDbContext = _context.Hotel.Include(h => h.HotelChain);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexChain(string name)
        {
            var hotels = await _context.Hotel.Include(h => h.HotelChain)
                .Where(co => co.Hotel_chainName_ID == name)
                .ToListAsync();

            return PartialView(hotels);
        }

        // GET: Hotel/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Hotel == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotel
                .Include(h => h.HotelChain)
                .FirstOrDefaultAsync(m => m.Hotel_ID == id);
            if (hotels == null)
            {
                return NotFound();
            }

            return View(hotels);
        }

        // GET: Hotel/Create
        public IActionResult Create()
        {
            ViewData["Hotel_chainName_ID"] = new SelectList(_context.HotelChain, "Name", "Name");
            return View();
        }

        // POST: Hotel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hotel_ID,Name,Hotel_chainName_ID,Street,City,Province,PostalCode,Email,RoomsCount,Rating")] Hotels hotels)
        {
            ModelState.Remove("Rooms");
            ModelState.Remove("HotelChain");
            ModelState.Remove("HotelPhoneNumbers");
            ModelState.Remove("Hotel_ID");
            if (ModelState.IsValid)
            {
                var hotelC = await _context.HotelChain.FindAsync(hotels.Hotel_chainName_ID);
                hotels.HotelChain = hotelC;
                hotels.Hotel_ID = hotels.Hotel_chainName_ID + " " + hotels.Street;
                _context.Add(hotels);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Hotel");
            }
            ViewData["Hotel_chainName_ID"] = new SelectList(_context.HotelChain, "Name", "Name", hotels.Hotel_chainName_ID);
            return View(hotels);
        }

        // GET: Hotel/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Hotel == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotel.FindAsync(id);
            if (hotels == null)
            {
                return NotFound();
            }
            ViewData["Hotel_chainName_ID"] = new SelectList(_context.HotelChain, "Name", "Name", hotels.Hotel_chainName_ID);
            return View(hotels);
        }

        // POST: Hotel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Hotel_ID,Name,Hotel_chainName_ID,Street,City,Province,PostalCode,Email,RoomsCount,Rating")] Hotels hotels)
        {
            if (id != hotels.Hotel_ID)
            {
                return NotFound();
            }
            ModelState.Remove("Rooms");
            ModelState.Remove("HotelChain");
            ModelState.Remove("HotelPhoneNumbers");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelsExists(hotels.Hotel_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "HotelChain");
            }
            ViewData["Hotel_chainName_ID"] = new SelectList(_context.HotelChain, "Name", "Name", hotels.Hotel_chainName_ID);
            return View(hotels);
        }

        // GET: Hotel/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Hotel == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotel
                .Include(h => h.HotelChain)
                .FirstOrDefaultAsync(m => m.Hotel_ID == id);
            if (hotels == null)
            {
                return NotFound();
            }
            return View(hotels);
        }

        // POST: Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Hotel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Hotel'  is null.");
            }
            var hotels = await _context.Hotel.FindAsync(id);
            if (hotels != null)
            {
                _context.Hotel.Remove(hotels);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "HotelChain");
        }

        private bool HotelsExists(string id)
        {
          return (_context.Hotel?.Any(e => e.Hotel_ID == id)).GetValueOrDefault();
        }
    }
}
