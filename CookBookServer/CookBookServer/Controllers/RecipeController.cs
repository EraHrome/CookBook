using Microsoft.AspNetCore.Authorization;
using CookBookServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using CookBookServer.Providers;
using Mongo.Repositories;
using CookBookServer.Services;
using System.Threading.Tasks;

namespace CookBookServer.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {

        public readonly CookieProvider _cookieProvider;
        public readonly UserRepository _userRepository;
        public readonly AuthRepository _authRepository;
        public readonly ApiService _apiService;

        public RecipeController(CookieProvider cookieProvider, UserRepository userRepository,
            AuthRepository authRepository, ApiService apiService)
        {
            _cookieProvider = cookieProvider;
            _userRepository = userRepository;
            _authRepository = authRepository;
            _apiService = apiService;
        }

        public IActionResult Index(string id)
        {

            var recipe = _apiService.GetRecipeById(id)?.Result;
            return View(recipe);

        }
        public IActionResult Constructor()
        {
            return View();
        }

        public async Task<IActionResult> Recipes()
        {
            var guid = _cookieProvider.GetGuidFromCookies(HttpContext);
            var auth = _authRepository.LoginnedByToken(guid);
            var user = _userRepository.GetById(auth.UserId);


            var recipes = await _apiService.GetManyRecipiesByIds(user.RecipesIds);

            return View(recipes);
        }

    }
}
