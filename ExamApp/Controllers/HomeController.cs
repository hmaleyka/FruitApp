using ExamApp.Context;
using ExamApp.Models;
using ExamApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                fruits = _context.fruits.ToList(),
            };
            return View(homeVM);
        }

      
    }
}
