using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eHotels.Areas.Identity.Data;
using eHotels.Services;
using eHotels.Models;

namespace eHotels.Controllers
{
    public class RentingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IUserService _userService;

        public RentingController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Renting
        public async Task<IActionResult> Index()
        {
              return _context.Renting != null ? 
                          View(await _context.Renting.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Renting'  is null.");
        }

        // GET: Renting/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Renting == null)
            {
                return NotFound();
            }

            var rentings = await _context.Renting
                .FirstOrDefaultAsync(m => m.RentingID == id);
            if (rentings == null)
            {
                return NotFound();
            }

            return View(rentings);
        }

        // GET: Renting/Create
        public async Task<IActionResult> Create(int? bookingId)
        {
            if (bookingId == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            var rentings = new Rentings
            {
                RoomNumber = booking.RoomID,
                Customer = booking.Customer,
                Start = booking.Start,
                End = booking.End
            };

            return View(rentings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentingID,RoomNumber,Employee,Customer,Start,End,Active")] Rentings rentings)
        {
            ModelState.Remove("Employee");
            string connectedUser = _userService.getUserId();
            rentings.Employee = connectedUser + "";
            if (ModelState.IsValid)
            {
                _context.Add(rentings);
                var booking = await _context.Booking.FindAsync(rentings.RoomNumber);
                booking.Active = true;
                _context.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexEmployee", "Booking");
            }
            return View(rentings);
        }

        // GET: Renting/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Renting == null)
            {
                return NotFound();
            }

            var rentings = await _context.Renting.FindAsync(id);
            if (rentings == null)
            {
                return NotFound();
            }
            return View(rentings);
        }

        // POST: Renting/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentingID,RoomNumber,Employee,Customer,Start,End,Active")] Rentings rentings)
        {
            if (id != rentings.RentingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentingsExists(rentings.RentingID))
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
            return View(rentings);
        }

        // GET: Renting/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Renting == null)
            {
                return NotFound();
            }

            var rentings = await _context.Renting
                .FirstOrDefaultAsync(m => m.RentingID == id);
            if (rentings == null)
            {
                return NotFound();
            }

            return View(rentings);
        }

        // POST: Renting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Renting == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Renting'  is null.");
            }
            var rentings = await _context.Renting.FindAsync(id);
            if (rentings != null)
            {
                _context.Renting.Remove(rentings);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentingsExists(int id)
        {
          return (_context.Renting?.Any(e => e.RentingID == id)).GetValueOrDefault();
        }
    }
}
