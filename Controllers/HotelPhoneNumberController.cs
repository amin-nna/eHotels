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
    public class HotelPhoneNumberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelPhoneNumberController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HotelPhoneNumber
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HotelPhoneNumber.Include(h => h.Hotel);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> IndexHotel(string hotel_ID)
        {
            var pHnumbers = await _context.HotelPhoneNumber
                .Where(co => co.Hotel_Hotel_ID == hotel_ID)
                .ToListAsync();

            return PartialView(pHnumbers);
        }

        // GET: HotelPhoneNumber/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.HotelPhoneNumber == null)
            {
                return NotFound();
            }

            var hotelPhoneNumbers = await _context.HotelPhoneNumber
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(m => m.ContactName == id);
            if (hotelPhoneNumbers == null)
            {
                return NotFound();
            }

            return View(hotelPhoneNumbers);
        }

        // GET: HotelPhoneNumber/Create
        public IActionResult Create()
        {
            ViewData["Hotel_Hotel_ID"] = new SelectList(_context.Hotel, "Hotel_ID", "Hotel_ID");
            return View();
        }

        // POST: HotelPhoneNumber/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactName,PhoneNumber,Hotel_Hotel_ID")] HotelPhoneNumbers hotelPhoneNumbers)
        {
            ModelState.Remove("Hotel");
            if (ModelState.IsValid)
            {
                _context.Add(hotelPhoneNumbers);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "HotelChain");
            }
            ViewData["Hotel_Hotel_ID"] = new SelectList(_context.Hotel, "Hotel_ID", "Hotel_ID", hotelPhoneNumbers.Hotel_Hotel_ID);
            return View(hotelPhoneNumbers);
        }

        // GET: HotelPhoneNumber/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.HotelPhoneNumber == null)
            {
                return NotFound();
            }

            var hotelPhoneNumbers = await _context.HotelPhoneNumber.FindAsync(id);
            if (hotelPhoneNumbers == null)
            {
                return NotFound();
            }
            ViewData["Hotel_Hotel_ID"] = new SelectList(_context.Hotel, "Hotel_ID", "Hotel_ID", hotelPhoneNumbers.Hotel_Hotel_ID);
            return View(hotelPhoneNumbers);
        }

        // POST: HotelPhoneNumber/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ContactName,PhoneNumber,Hotel_Hotel_ID")] HotelPhoneNumbers hotelPhoneNumbers)
        {
            if (id != hotelPhoneNumbers.ContactName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelPhoneNumbers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelPhoneNumbersExists(hotelPhoneNumbers.ContactName))
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
            ViewData["Hotel_Hotel_ID"] = new SelectList(_context.Hotel, "Hotel_ID", "Hotel_ID", hotelPhoneNumbers.Hotel_Hotel_ID);
            return View(hotelPhoneNumbers);
        }

        // GET: HotelPhoneNumber/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.HotelPhoneNumber == null)
            {
                return NotFound();
            }

            var hotelPhoneNumbers = await _context.HotelPhoneNumber
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(m => m.ContactName == id);
            if (hotelPhoneNumbers == null)
            {
                return NotFound();
            }

            return View(hotelPhoneNumbers);
        }

        // POST: HotelPhoneNumber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.HotelPhoneNumber == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HotelPhoneNumber'  is null.");
            }
            var hotelPhoneNumbers = await _context.HotelPhoneNumber.FindAsync(id);
            if (hotelPhoneNumbers != null)
            {
                _context.HotelPhoneNumber.Remove(hotelPhoneNumbers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "HotelChain");
        }

        private bool HotelPhoneNumbersExists(string id)
        {
          return (_context.HotelPhoneNumber?.Any(e => e.ContactName == id)).GetValueOrDefault();
        }
    }
}
