using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eHotels.Models;
using Microsoft.EntityFrameworkCore;
using eHotels.Areas.Identity.Data;
using Microsoft.Build.Framework;

namespace eHotels.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult RoomsArea()
    {
        var viewresult = _context.View_number_of_available_rooms_per_area.FromSqlRaw("select * from [View_number_of_available_rooms_per_area];").ToList();
        return PartialView(viewresult);
    }

    
    public IActionResult HotelCapacity()
    {
        var viewresult = _context.View_hotel_capacity.FromSqlRaw("select * from [View_hotel_capacity];").ToList();
        return PartialView(viewresult);
    }

    public IActionResult GoBack()
    {
        string previousUrl = Request.Headers["Referer"].ToString();
        if (string.IsNullOrEmpty(previousUrl))
        {
            // If there is no previous page, redirect to the home page
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // Otherwise, redirect to the previous page
            return Redirect(previousUrl);
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

