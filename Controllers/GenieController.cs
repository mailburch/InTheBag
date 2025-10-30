using Microsoft.AspNetCore.Mvc;

namespace InTheBag.Controllers
{
    public class GenieController : Controller
    {
        // GET: /Genie/Create
        public IActionResult Create() => View();

        // POST: /Genie/Create  (primitive binding via parameters)
        [HttpPost]
        public IActionResult Create(string GenieName, int Age, int WishesGranted)
        {
            if (WishesGranted > 5000 || Age > 1000)
                return View("ExperiencedGenie");
            else
                return View("Novice");
        }

        // POST: /Genie/Create  (Request.Form version for Hands On #2)
        // Keep the above method; comment it out when testing this one.
        // [HttpPost]
        // public IActionResult Create(string GenieName)
        // {
        //     int numGranted = int.Parse(Request.Form["WishesGranted"]);
        //     int years = int.Parse(Request.Form["Age"]);
        //     if (numGranted > 5000 || years > 1000)
        //         return View("ExperiencedGenie");
        //     else
        //         return View("Novice");
        // }

        // GET via RouteData: /Genie/Create2/{GenieName?}/{Age?}/{WishesGranted?}
        public IActionResult Create2()
        {
            var name = (string?)RouteData.Values["GenieName"] ?? "";
            int age = int.Parse((string)RouteData.Values["Age"]);
            int wishes = int.Parse((string)RouteData.Values["WishesGranted"]);

            if (wishes > 5000 || age > 1000)
                return View("ExperiencedGenie");
            else
                return View("Novice");
        }

        // GET: /Genie/Perks
        [HttpGet]
        public IActionResult Perks()
        {
            ViewBag.Posted = false;
            return View();
        }

        // POST: /Genie/Perks  (array binding)
        [HttpPost]
        public IActionResult Perks(string[] perk)
        {
            ViewBag.Posted = true;
            ViewBag.Perks = perk; // or: ViewBag.Perks = Request.Form["perk"];
            return View();
        }
    }
}
