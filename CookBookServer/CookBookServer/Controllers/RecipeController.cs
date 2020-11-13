using Microsoft.AspNetCore.Mvc;

namespace CookBookServer.Controllers
{
    public class RecipeController : Controller
    {
        // GET: RecipeController
        public IActionResult Index()
        {
            return View();
        }
    }
}
