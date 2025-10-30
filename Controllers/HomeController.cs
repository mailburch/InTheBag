using InTheBag.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InTheBag.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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
    }
}