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
            int? num = HttpContext.Session.GetInt32("num") ?? 0;
            num++;
            HttpContext.Session.SetInt32("num", (int)num);

            ViewBag.Message = $"Session count: {num}";
            return View();
        }

        // ===== NEW: WishIndex (reads serialized object) =====
        public IActionResult WishIndex()
        {
            string? json = HttpContext.Session.GetString("myWishes");

            Wishes wishes;
            if (json != null)
            {
                wishes = JsonSerializer.Deserialize<Wishes>(json)!;
            }
            else
            {
                wishes = new Wishes
                {
                    wish1 = "Travel the world",
                    wish2 = "Own a cabin in the woods",
                    wish3 = "Learn to play piano"
                };

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

        // CHAPTER 10 (Hands On #3): bind via Request.Form (primitive types), not model
        [HttpPost]
        public IActionResult NewWishIndex(int? ID)
        {
            var myWishes = new Wishes
            {
                ID = ID ?? 0,
                wish1 = Request.Form["wish1"],
                wish2 = Request.Form["wish2"],
                wish3 = Request.Form["wish3"]
            };

            string json = JsonSerializer.Serialize(myWishes);
            HttpContext.Session.SetString("myWishes", json);

            // you can pass the model or just redirect to load from session
            return RedirectToAction("WishIndex");
            // return View("WishIndex", myWishes);
        }
    }
}
