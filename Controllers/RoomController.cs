using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eHotels.Areas.Identity.Data;
using eHotels.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using eHotels.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eHotels.Controllers
{
    public class RoomController : Controller
    {
        private ApplicationDbContext _context;

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

        /*
        public async Task<IActionResult> IndexSearchFilter()
        {
            var applicationDbContext = _context.Room.Include(h => h.Hotel);
            return View(await applicationDbContext.ToListAsync());
        }
        */
        public async Task<IActionResult> IndexSearchFilter(string? searchTerm, int? minPrice, int? maxPrice, string? city, DateTime? startDate, DateTime? endDate, string? hotel, int? capacity, int? category)
        {
            ViewData["HotelName"] = new SelectList(_context.Hotel, "Name", "Name");
            var query = _context.Room.Include(p => p.Hotel).AsQueryable();

            // apply search term filter if provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Hotel.Name.Contains(searchTerm) || p.RoomAmenities.Any(a => a.Description.Contains(searchTerm)));
            }

            // apply price range filter if provided
            if (minPrice > 0)
            {
                query = query.Where(p => p.Price >= minPrice);
            }
            if (maxPrice > 0)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }

            // apply category filter if provided
            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(p => p.Hotel.City == city);
            }

            // apply start and end date filter if provided
            //Here roomNumber stands for RoomID
            if (startDate.HasValue && endDate.HasValue)
            {
                var bookedRooms = await _context.Booking
                    .Where(b => (b.Start <= startDate && b.End >= startDate)
                        || (b.Start <= endDate && b.End >= endDate)
                        || (b.Start >= startDate && b.End <= endDate))
                    .Select(b => b.RoomNumber)
                    .ToListAsync();

                var rentedRooms = await _context.Renting
                    .Where(r => (r.Start <= startDate && r.End >= startDate)
                        || (r.Start <= endDate && r.End >= endDate)
                        || (r.Start >= startDate && r.End <= endDate))
                    .Select(r => r.RoomNumber)
                    .ToListAsync();

                query = query.Where(r => !bookedRooms.Contains(r.RoomNumber) && !rentedRooms.Contains(r.RoomNumber));
            }

            // apply hotel filter if provided
            if (!string.IsNullOrEmpty(hotel))
            {
                query = query.Where(p => p.Hotel.Name == hotel);
            }

            // apply capacity filter if provided
            if (capacity.HasValue)
            {
                query = query.Where(p => p.Capacity == capacity.Value);
            }

            // apply category filter if provided
            if (category.HasValue)
            {
                switch (category.Value)
                {
                    case 1:
                        query = query.Where(p => p.Hotel.Rating == 1);
                        break;
                    case 2:
                        query = query.Where(p => p.Hotel.Rating == 2);
                        break;
                    case 3:
                        query = query.Where(p => p.Hotel.Rating == 3);
                        break;
                    case 4:
                        query = query.Where(p => p.Hotel.Rating == 4);
                        break;
                    case 5:
                        query = query.Where(p => p.Hotel.Rating == 5);
                        break;
                }
            }

            var products = await query.ToListAsync();

            return View(products);
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
            
            ModelState.Remove("Hotel");
            ModelState.Remove("RoomIssues");
            ModelState.Remove("RoomID");
            ModelState.Remove("RoomAmenities");
            ModelState.Remove("Bookings");
            ModelState.Remove("Rentings");
            if (ModelState.IsValid)
            {
                var hotelR = await _context.Hotel.FindAsync(rooms.Hotel_ID);
                rooms.Hotel = hotelR;
                rooms.Hotel_ID=rooms.Hotel.Hotel_ID +" "+rooms.Hotel.Street;
                rooms.RoomID = rooms.Hotel_ID + " " + rooms.RoomNumber;
                _context.Add(rooms);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Room");
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

            var rooms = await _context.Room.Include(r => r.Hotel).FirstOrDefaultAsync(r => r.RoomID == id);
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
            ModelState.Remove("Hotel");
            ModelState.Remove("RoomIssues");
            ModelState.Remove("Employee");

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
                return RedirectToAction("Index", "HotelChain");
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
                // Declare a table variable to store the deleted Room IDs
                _context.Database.ExecuteSqlRaw("DECLARE @DeletedRowsRoom TABLE (RoomID nvarchar(450)) " +
                                                "SET NOCOUNT ON; " +
                                                "DELETE FROM [Room] OUTPUT deleted.RoomID INTO @DeletedRowsRoom WHERE [RoomID] = @p0",
                    new SqlParameter("@p0", id));

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "HotelChain");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "HotelChain");
        }

        private bool RoomsExists(string id)
        {
          return (_context.Room?.Any(e => e.RoomID == id)).GetValueOrDefault();
        }
    }
}
