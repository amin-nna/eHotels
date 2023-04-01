using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eHotels.Areas.Identity.Data;
using eHotels.Models;
using eHotels.Services;
using System.Security.Cryptography;
using Twilio.TwiML.Voice;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace eHotels.Controllers
{
    public class BookingController : Controller
    {
        private  ApplicationDbContext _context;
        private IUserService _userService;

        public BookingController(ApplicationDbContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
              return _context.Booking != null ? 
                          View(await _context.Booking.Include(h => h.Room).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Booking'  is null.");
        }

        public async Task<IActionResult> IndexClient()
        {
            string client = _userService.getUserId();
            var bookings = await _context.Booking
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Where(b => b.Customer == client)
                .ToListAsync();

            return View(bookings);
        }

        //Non rented booking 
        public async Task<IActionResult> IndexEmployee()
        {
            
            var bookings = await _context.Booking
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Where(b => b.Active == false)
                .ToListAsync();

            return View(bookings);
        }




        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var bookings = await _context.Booking
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Booking/Create
        [Authorize]
        public async Task<IActionResult> Create(string roomID)
        {
            var room = await _context.Room.FindAsync(roomID);

            if (room == null)
            {
                return NotFound();
            }

            var booking = new Bookings { RoomNumber = room.RoomID };

            // Get booked dates for the room
            var bookedDates = await _context.Booking
                .Where(b => b.RoomNumber == roomID && b.End > DateTime.Now)
                .Select(b => new { b.Start, b.End })
                .ToListAsync();

            // Get rented dates for the room
            var rentedDates = await _context.Renting
                .Where(r => r.RoomNumber == roomID && r.End > DateTime.Now)
                .Select(r => new { r.Start, r.End })
                .ToListAsync();

            // Add booked and rented dates to ViewBag
            ViewBag.BookedDates = JsonConvert.SerializeObject(bookedDates);
            ViewBag.RentedDates = JsonConvert.SerializeObject(rentedDates);


            return View(booking);
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(string roomID, [Bind("BookingID,Customer,RoomNumber,Employee,Start,End,Active")] Bookings bookings)
        {
            var room = await _context.Room.FindAsync(roomID);
            if (room == null)
            {
                return NotFound();
            }

            ModelState.Remove("Customer");
            ModelState.Remove("Room");
            ModelState.Remove("RoomID");
            ModelState.Remove("Employee");
            ModelState.Remove("Active");

            // Get booked dates for the room
            var bookedDates = await _context.Booking
                .Where(b => b.RoomNumber == roomID )
                .Select(b => new { b.Start, b.End })
                .ToListAsync();

            // Get rented dates for the room
            var rentedDates = await _context.Renting
                .Where(r => r.RoomNumber == roomID )
                .Select(r => new { r.Start, r.End })
                .ToListAsync();

            // Add booked and rented dates to ViewBag
            ViewBag.BookedDates = JsonConvert.SerializeObject(bookedDates);
            ViewBag.RentedDates = JsonConvert.SerializeObject(rentedDates);


            if ( bookings.Start > bookings.End )
            {
                ViewBag.ErrorMessage="Your booking cannot end before it starts.";
                return View(bookings);
            }

            foreach (var date in bookedDates)
            {
 
                if (bookings.Start >= date.Start && bookings.Start<= date.End || bookings.End >= date.Start && bookings.End <= date.End)
                {
                    ViewBag.ErrorMessage="You choosed an unvailable date, please change your selection.";
                    return View(bookings);
                }
            }

            foreach (var date in rentedDates)
            {

                if (bookings.Start >= date.Start && bookings.Start <= date.End || bookings.End >= date.Start && bookings.End <= date.End)
                {
                    ViewBag.ErrorMessage="You choosed an unvailable date, please change your selection.";
                    return View(bookings);
                }
            }


            string connectedUser = _userService.getUserId();
            bookings.Customer = connectedUser + "";
            bookings.RoomNumber = room.RoomNumber;
            bookings.RoomID = room.RoomID;


            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }  

            return View(bookings);
        }

        // GET: Booking/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var bookings = await _context.Booking.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,Customer,RoomNumber,Employee,Start,End,Active")] Bookings bookings)
        {
            if (id != bookings.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.BookingID))
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
            return View(bookings);
        }

        // GET: Booking/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var bookings = await _context.Booking
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Booking == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Booking'  is null.");
            }
            var bookings = await _context.Booking.FindAsync(id);
            if (bookings != null)
            {
                _context.Booking.Remove(bookings);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
          return (_context.Booking?.Any(e => e.BookingID == id)).GetValueOrDefault();
        }
    }
}
