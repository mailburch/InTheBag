using InTheBag.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace InTheBag.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // ===== Default pages =====
        public IActionResult Index()
        {
            return View();
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

        // ===== Examples using ViewBag, ViewData, TempData =====
        public IActionResult IndexViewBag()
        {
            ViewBag.Title = "Index (ViewBag)";
            ViewBag.Message = "Hello from ViewBag";
            ViewBag.Count = 3;
            return View();
        }

        public IActionResult IndexViewData()
        {
            ViewData["Title"] = "Index (ViewData)";
            ViewData["Message"] = "Hello from ViewData";
            ViewData["Count"] = 5;
            return View();
        }

        public IActionResult IndexTempData()
        {
            TempData["Title"] = "Index (TempData)";
            TempData["Message"] = "Hello from TempData";
            TempData["Count"] = 7;
            return View();
        }

        // ===== NEW: Session State Example =====
        public IActionResult IndexSession()
        {
            // Try to get number; if missing, start from 0
            int? num = HttpContext.Session.GetInt32("num") ?? 0;
            num++;
            HttpContext.Session.SetInt32("num", (int)num);

            ViewBag.Message = $"Session count: {num}";
            return View();
        }

        // ===== NEW: WishIndex (reads serialized object) =====
        public IActionResult WishIndex()
        {
            // Try to get serialized wishes
            string? json = HttpContext.Session.GetString("myWishes");

            Wishes wishes;
            if (json != null)
            {
                wishes = JsonSerializer.Deserialize<Wishes>(json)!;
            }
            else
            {
                // Default wishes
                wishes = new Wishes
                {
                    wish1 = "Travel the world",
                    wish2 = "Own a cabin in the woods",
                    wish3 = "Learn to play piano"
                };

                // Save defaults to session
                string saveJson = JsonSerializer.Serialize(wishes);
                HttpContext.Session.SetString("myWishes", saveJson);
            }

            return View(wishes);
        }

        // ===== NEW: Create New Wishes (GET + POST) =====
        [HttpGet]
        public IActionResult NewWishIndex()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewWishIndex(Wishes wish)
        {
            if (ModelState.IsValid)
            {
                string json = JsonSerializer.Serialize(wish);
                HttpContext.Session.SetString("myWishes", json);
                return RedirectToAction("WishIndex");
            }

            return View(wish);
        }
    }
}
