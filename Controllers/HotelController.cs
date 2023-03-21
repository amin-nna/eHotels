using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eHotels.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using eHotels.Models;
using System.Net;
using Newtonsoft.Json;

namespace eHotels.Controllers
{
    [Authorize(Roles = "Administrator, Employee")]
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public HotelController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Hotel
        public async Task<IActionResult> Index()
        {
              return _context.Hotel != null ? 
                          View(await _context.Hotel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Hotel'  is null.");
        }

        public async Task<IActionResult> IndexChain(string name)
        {
            var hotels = await _context.Hotel
                .Where(co => co.Hotel_chainName_ID == name)
                .ToListAsync();


            return PartialView(hotels);
        }


        // GET: Hotel/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Hotel == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotel
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
            ViewBag.HotelChainList = _context.HotelChain.Select(h => h.Name).ToList();

            return View();
        }

        // POST: Hotel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hotel_ID,Hotel_chainName_ID,Name,Street,City,Province,PostalCode,Email,RoomsCount")] Hotels hotels)
        {
            ViewBag.HotelChainList = _context.HotelChain.Select(h => h.Name).ToList();

            ModelState.Remove("HotelChain");
            ModelState.Remove("Rooms");
            ModelState.Remove("HotelPhoneNumbers");
            ModelState.Remove("Hotel_ID");
            if (ModelState.IsValid)
            {
                hotels.Hotel_ID = hotels.Hotel_chainName_ID + " " + hotels.Name + " " + hotels.Street;
                _context.Add(hotels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotels);
        }

        // GET: Hotel/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(hotels);
        }

        // POST: Hotel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Hotel_ID,Hotel_chainName_ID,Street,City,Province,PostalCode,Email,RoomsCount")] Hotels hotels)
        {
            if (id != hotels.Hotel_ID)
            {
                return NotFound();
            }

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
                return RedirectToAction(nameof(Index));
            }
            return View(hotels);
        }

        // GET: Hotel/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Hotel == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotel
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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
            return RedirectToAction(nameof(Index));
        }

        private bool HotelsExists(string id)
        {
          return (_context.Hotel?.Any(e => e.Hotel_ID == id)).GetValueOrDefault();
        }

        public IActionResult FilterHotels(string address, int perimeter, string unit)
        {
            var hotels = _context.Hotel.ToList();
            var filteredHotels = new List<Hotels>();

            foreach (var hotel in hotels)
            {
                var fullAddress = hotel.Street + ", " + hotel.City + ", " + hotel.Province + " " + hotel.PostalCode;
                var distance = CalculateDistance(address, fullAddress, unit);

                if (distance <= perimeter)
                {
                    filteredHotels.Add(hotel);
                }
            }

            return View(filteredHotels);
        }

        private double CalculateDistance(string originAddress, string destinationAddress, string unit)
        {
            var requestUri = string.Format("https://maps.googleapis.com/maps/api/distancematrix/json?origins={0}&destinations={1}&key={2}",
                originAddress, destinationAddress, _configuration.GetValue<string>("GoogleMapsApiKey"));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var json = string.Empty;

            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }

            var matrix = JsonConvert.DeserializeObject<DistanceMatrix>(json);
            var distance = matrix.Rows.First().Elements.First().Distance.Value;

            if (unit == "km")
            {
                distance /= 1000;
            }
            else if (unit == "mi")
            {
                distance /= 1609.34;
            }

            return distance;
        }

        public class DistanceMatrix
        {
            public List<Row> Rows { get; set; }
        }

        public class Row
        {
            public List<Element> Elements { get; set; }
        }

        public class Element
        {
            public Distance Distance { get; set; }
        }

        public class Distance
        {
            public double Value { get; set; }
        }


    }
}
