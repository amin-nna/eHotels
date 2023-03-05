using System.Diagnostics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eHotels.Areas.Identity.Data;
using eHotels.Models;
using eHotels.Services;

namespace eHotels.Controllers;

public class DashboardController : Controller
{
    
    private readonly ApplicationDbContext _context;
    private IUserService _userService;

    public DashboardController(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    //Nous renvoies vers les vues respectives

    //Index du Dashboard
    public IActionResult Index()
    {
        return View();
    }

    //Vue du chat
    public IActionResult Inbox()
    {
        return View();
    }




    //Vue de la page pour gérer l'abonnement
    public IActionResult MySubscription()
    {
        return View();
    }

   

    

    //
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

