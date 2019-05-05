using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using webapp.Models;

namespace webapp.Controllers
{

    [AllowAnonymous]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.Message = "A message from the HomeController";
            return View();
        }

        [Route("privacy")]
        [Route("home/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
