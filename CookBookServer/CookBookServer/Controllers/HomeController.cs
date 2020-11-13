using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using MyCodeServer.Providers;
using CookBookServer.Models;
using System.Diagnostics;
using CookBookServer.Attributes;

namespace CookBookServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CookieProvider _cookieProvider;

        public HomeController(ILogger<HomeController> logger, CookieProvider cookieProvider)
        {
            _cookieProvider = cookieProvider;
            _logger = logger;
        }

        [Authentifaction]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Authorization()
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
    }
}
