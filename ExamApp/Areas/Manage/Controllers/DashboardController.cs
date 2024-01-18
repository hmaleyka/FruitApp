using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [AutoValidateAntiforgeryToken]
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
