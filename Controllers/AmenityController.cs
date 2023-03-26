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
    public class AmenityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AmenityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Amenity
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoomAmenity.Include(r => r.Room);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> IndexRoom(string Room_ID)
        {
            var amenities = await _context.RoomAmenity
                .Where(co => co.RoomNumber == Room_ID)
                .ToListAsync();

            return PartialView(amenities);
        }

        // GET: Amenity/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.RoomAmenity == null)
            {
                return NotFound();
            }

            var roomAmenities = await _context.RoomAmenity
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomNumber == id);
            if (roomAmenities == null)
            {
                return NotFound();
            }

            return View(roomAmenities);
        }

        // GET: Amenity/Create
        public IActionResult Create()
        {
            ViewData["RoomNumber"] = new SelectList(_context.Room, "RoomID", "RoomID");
            return View();
        }

        // POST: Amenity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomNumber,Amenity,Description")] RoomAmenities roomAmenities)
        {
            ModelState.Remove("Room");
            if (ModelState.IsValid)
            {
                _context.Add(roomAmenities);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Issue");
            }
            ViewData["RoomNumber"] = new SelectList(_context.Room, "RoomID", "RoomID", roomAmenities.RoomNumber);
            return View(roomAmenities);
        }

        // GET: Amenity/Edit/5
        public async Task<IActionResult> Edit(string roomNumber, string amenity)
        {
            if (roomNumber == null || amenity == null)
            {
                return NotFound();
            }

            var roomIssues = await _context.RoomAmenity.FindAsync(roomNumber, amenity);

            if (roomIssues == null)
            {
                return NotFound();
            }

            ViewData["RoomNumber"] = new SelectList(_context.Room, "RoomNumber", "RoomNumber", roomIssues.RoomNumber);
            return View(roomIssues);
        }

        // POST: Amenity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string roomNumber, string amenity, [Bind("RoomNumber,Amenity,Description")] RoomAmenities roomAmenities)
        {
            if (roomNumber != roomAmenities.RoomNumber)
            {
                return NotFound();
            }
            ModelState.Remove("Room");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomAmenities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomAmenitiesExists(roomAmenities.RoomNumber, roomAmenities.Amenity))
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
            ViewData["RoomNumber"] = new SelectList(_context.Room, "RoomID", "RoomID", roomAmenities.RoomNumber);
            return View(roomAmenities);
        }
        // GET: Issue/Delete/5
        public async Task<IActionResult> Delete(string roomNumber, string amenity)
        {
            if (roomNumber == null || amenity == null || _context.RoomAmenity == null)
            {
                return NotFound();
            }

            var roomIssues = await _context.RoomAmenity
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomNumber == roomNumber && m.Amenity == amenity);
            if (roomIssues == null)
            {
                return NotFound();
            }

            return View(roomIssues);
        }

        // POST: Issue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string roomNumber, string amenity)
        {
            if (_context.RoomAmenity == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RoomIssue' is null.");
            }

            var roomIssues = await _context.RoomAmenity.FindAsync(roomNumber, amenity);
            if (roomIssues != null)
            {
                _context.RoomAmenity.Remove(roomIssues);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "HotelChain");
        }

        private bool RoomAmenitiesExists(string roomNumber, string amenity)
        {
            return (_context.RoomAmenity?.Any(e => e.RoomNumber == roomNumber && e.Amenity == amenity)).GetValueOrDefault();
        }

    }
}
