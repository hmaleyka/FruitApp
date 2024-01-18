using ExamApp.Areas.Manage.ViewModels.FruitVM;
using ExamApp.Context;
using ExamApp.Helpers;
using ExamApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [AutoValidateAntiforgeryToken]
    public class FruitController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FruitController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<Fruit> fruits = _context.fruits.ToList();
            return View(fruits);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create ()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateFruitVM fruitvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //if (fruitvm.Image.CheckType("image/"))
            //{
            //    ModelState.AddModelError("Image", "The type of image is not okay");
            //}
            //if (fruitvm.Image.CheckLong(2097152))
            //{
            //    ModelState.AddModelError("Image", "The length of image should not be longer than 2mb");
            //}
            Fruit fruit = new Fruit()
            {
                Title = fruitvm.Title,
                Subtitle = fruitvm.Subtitle,
                ImgUrl = fruitvm.Image.Upload(_env.WebRootPath, @"\Upload\Fruit\")
            };

            await _context.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Update (int id)
        {
            Fruit fruit = _context.fruits.Where(x=>x.Id == id).FirstOrDefault();
            UpdateFruitVM fruitVM  = new UpdateFruitVM()
            {
                Id= id,
                Title= fruit.Title,
                Subtitle = fruit.Subtitle,
                ImgUrl = fruit.ImgUrl,
            };
            return View(fruitVM);  
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(int id , UpdateFruitVM fruitvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Fruit fruit = await _context.fruits.Where(x => x.Id == id).FirstOrDefaultAsync();

            //if (fruitvm.Image.CheckType("image /"))
            //{
            //    ModelState.AddModelError("Image", "The type of image is not okay");
            //}
            //if (fruitvm.Image.CheckLong(2097152))
            //{
            //    ModelState.AddModelError("Image", "The length of image should not be longer than 2mb");
            //}
            fruit.Title = fruitvm.Title;
            fruit.Subtitle = fruitvm.Subtitle;
            fruit.ImgUrl = fruitvm.Image.Upload(_env.WebRootPath, @"\Upload\Fruit\");
            _context.Update(fruit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete (int id)
        {
            Fruit fruit = _context.fruits.FirstOrDefault(x => x.Id == id);
            if (fruit == null)
            {
                return View(fruit);
            }
            _context.fruits.Remove(fruit);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
