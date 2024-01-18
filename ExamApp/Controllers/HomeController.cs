using ExamApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamApp.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
