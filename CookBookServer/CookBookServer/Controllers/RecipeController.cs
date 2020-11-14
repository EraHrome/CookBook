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
            var keke = User.Identity.IsAuthenticated;
            return View();
        }
    }
}
