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
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Room
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Room.Include(r => r.Hotel);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexHotel(string hotel_ID)
        {
            var rooms = await _context.Room
                .Where(co => co.Hotel_ID == hotel_ID)
                .ToListAsync();

            return PartialView(rooms);
        }
            // GET: Room/Details/5
            public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Room == null)
            {
                return NotFound();
            }

            var rooms = await _context.Room
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }

        // GET: Room/Create
        public IActionResult Create()
        {
            ViewData["Hotel_ID"] = new SelectList(_context.Hotel, "Hotel_ID", "Hotel_ID");
            return View();
        }

        // POST: Room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomID,RoomNumber,Hotel_ID,Price,Currency,Capacity,Extendable,View")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rooms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hotel_ID"] = new SelectList(_context.Hotel, "Hotel_ID", "Hotel_ID", rooms.Hotel_ID);
            return View(rooms);
        }

        // GET: Room/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Room == null)
            {
                return NotFound();
            }

            var rooms = await _context.Room.FindAsync(id);
            if (rooms == null)
            {
                return NotFound();
            }
            ViewData["Hotel_ID"] = new SelectList(_context.Hotel, "Hotel_ID", "Hotel_ID", rooms.Hotel_ID);
            return View(rooms);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RoomID,RoomNumber,Hotel_ID,Price,Currency,Capacity,Extendable,View")] Rooms rooms)
        {
            if (id != rooms.RoomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsExists(rooms.RoomID))
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
            ViewData["Hotel_ID"] = new SelectList(_context.Hotel, "Hotel_ID", "Hotel_ID", rooms.Hotel_ID);
            return View(rooms);
        }

        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Room == null)
            {
                return NotFound();
            }

            var rooms = await _context.Room
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Room == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Room'  is null.");
            }
            var rooms = await _context.Room.FindAsync(id);
            if (rooms != null)
            {
                _context.Room.Remove(rooms);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomsExists(string id)
        {
          return (_context.Room?.Any(e => e.RoomID == id)).GetValueOrDefault();
        }
    }
}
