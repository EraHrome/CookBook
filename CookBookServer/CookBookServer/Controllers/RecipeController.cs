using Microsoft.AspNetCore.Authorization;
using CookBookServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using CookBookServer.Providers;
using Mongo.Repositories;

namespace CookBookServer.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {

        public readonly CookieProvider _cookieProvider;
        public readonly UserRepository _userRepository;
        public readonly AuthRepository _authRepository;
        public readonly RecipesRepository _recipesRepository;

        public RecipeController(CookieProvider cookieProvider, UserRepository userRepository,
            AuthRepository authRepository, RecipesRepository recipesRepository)
        {
            _cookieProvider = cookieProvider;
            _userRepository = userRepository;
            _authRepository = authRepository;
            _recipesRepository = recipesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Constructor()
        {
            return View();
        }

        public IActionResult Recipes()
        {
            var guid = _cookieProvider.GetGuidFromCookies(HttpContext);
            var auth = _authRepository.LoginnedByToken(guid);
            var user = _userRepository.GetById(auth.UserId);
            var recipes = _recipesRepository.GetManyByUid(user.Id);
            return View(recipes);
        }

    }
}
