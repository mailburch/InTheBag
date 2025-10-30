using Microsoft.AspNetCore.Mvc;

namespace InTheBag.Controllers
{
    public class AllAboutResultsController : Controller
    {
        public IActionResult Index()
        {
            var weekday = DateTime.Now.DayOfWeek;
            var day = weekday.ToString();
            var time = DateTime.Now.Hour;

            if (time <= 6)
            {
                ViewBag.Greeting = "It is too early to be up!";
            }
            else if (time < 12)
            {
                ViewBag.Greeting = "Good Morning";
            }
            else if (time < 18)
            {
                ViewBag.Greeting = "Good Afternoon";
            }
            else
            {
                ViewBag.Greeting = "Good Evening";
            }

            int route = 0;

            switch (day)
            {
                case "Monday":
                case "Tuesday":
                    ViewData["dayMessage"] = "The work week just started! Stay focused, you have a lot to do this week!";
                    route = 1;
                    break;
                case "Wednesday":
                    ViewData["dayMessage"] = "Halfway to the weekend!";
                    route = 2;
                    break;
                case "Thursday":
                    ViewData["dayMessage"] = "Isn't it Friday somewhere?";
                    route = 3;
                    break;
                case "Friday":
                    ViewData["dayMessage"] = "Woo hoo TGIF!";
                    route = 4;
                    break;
                default:
                    ViewData["dayMessage"] = "Ahhhh the weekend!";
                    route = 5;
                    break;
            }

            if (route == 1)
            {
                return RedirectToAction("Weekday", "AllAboutResults");
            }
            else if (route == 2 || route == 3)
            {
                return Redirect("https://lisabalbach.com/CIT218/Chapter8/HappyWednesday.html");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Weekday()
        {
            ViewBag.Greeting = "Congratulations, the work week just started and you have been rerouted!";
            return View();
        }
    }
}
