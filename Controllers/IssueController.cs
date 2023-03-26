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
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Issue
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoomIssue.Include(r => r.Room);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexRoom(string Room_ID)
        {
            var issues = await _context.RoomIssue
                .Where(co => co.RoomNumber == Room_ID)
                .ToListAsync();

            return PartialView(issues);
        }

        // GET: Issue/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.RoomIssue == null)
            {
                return NotFound();
            }

            var roomIssues = await _context.RoomIssue
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomNumber == id);
            if (roomIssues == null)
            {
                return NotFound();
            }

            return View(roomIssues);
        }

        // GET: Issue/Create
        public IActionResult Create()
        {
            ViewData["RoomNumber"] = new SelectList(_context.Room, "RoomID", "RoomID");
            return View();
        }

        // POST: Issue/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomNumber,Problem,Description")] RoomIssues roomIssues)
        {
            ModelState.Remove("Room");
            if (ModelState.IsValid)
            {
                _context.Add(roomIssues);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Issue");
            }
            ViewData["RoomNumber"] = new SelectList(_context.Room, "RoomID", "RoomID", roomIssues.RoomNumber);
            return View(roomIssues);
        }

        public async Task<IActionResult> Edit(string roomNumber, string problem)
        {
            if (roomNumber == null || problem == null)
            {
                return NotFound();
            }

            var roomIssues = await _context.RoomIssue.FindAsync(roomNumber, problem);

            if (roomIssues == null)
            {
                return NotFound();
            }

            ViewData["RoomNumber"] = new SelectList(_context.Room, "RoomNumber", "RoomNumber", roomIssues.RoomNumber);
            return View(roomIssues);
        }

        // POST: Issue/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string roomNumber, string problem, [Bind("RoomNumber,Problem,Description")] RoomIssues roomIssues)
        {
            if (roomNumber != roomIssues.RoomNumber)
            {
                return NotFound();
            }
            ModelState.Remove("Room");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomIssues);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomIssuesExists(roomIssues.RoomNumber, roomIssues.Problem))
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
            ViewData["RoomNumber"] = new SelectList(_context.Room, "RoomID", "RoomID", roomIssues.RoomNumber);
            return View(roomIssues);
        }

        // GET: Issue/Delete/5
        public async Task<IActionResult> Delete(string roomNumber, string amenity)
        {
            if (roomNumber == null || amenity == null || _context.RoomIssue == null)
            {
                return NotFound();
            }

            var roomIssues = await _context.RoomIssue
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomNumber == roomNumber && m.Problem == amenity);
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
            if (_context.RoomIssue == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RoomIssue' is null.");
            }

            var roomIssues = await _context.RoomIssue.FindAsync(roomNumber, amenity);
            if (roomIssues != null)
            {
                _context.RoomIssue.Remove(roomIssues);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "HotelChain");
        }

        private bool RoomIssuesExists(string roomNumber, string amenity)
        {
            return (_context.RoomIssue?.Any(e => e.RoomNumber == roomNumber && e.Problem == amenity)).GetValueOrDefault();
        }

    }
}
