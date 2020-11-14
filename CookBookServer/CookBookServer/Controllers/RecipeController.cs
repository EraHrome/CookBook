using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBookServer.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        // GET: RecipeController
      
        public IActionResult Index()
        {
            return View();
        }
    }
}
