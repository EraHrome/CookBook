using Microsoft.AspNetCore.Authorization;
using CookBookServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using CookBookServer.Providers;
using Mongo.Repositories;
using CookBookServer.Services;
using System.Threading.Tasks;
using Mongo.Models.Recipe;
using System.Linq;
using System;
using System.Collections.Generic;

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

        [HttpPost]
        public async Task<IActionResult> Constructor(RecipeModel model)
        {           
            model.Id = Guid.NewGuid().ToString();
            model.Ingredients = model.Checkpoints.SelectMany(x => x.Ingredients != null && x.Ingredients.Any() ? x.Ingredients : new List<IngredientModel>()).ToList();
            model.PublicationDate = DateTime.Now;

            var totalTime = 0;
            foreach (var checkpoint in model.Checkpoints)
            {
                totalTime += checkpoint.CookingMinutes;
                checkpoint.CookingSeconds = (checkpoint.CookingMinutes * 60).ToString();
                checkpoint.TimerSeconds = (checkpoint.TimerMinutes * 60).ToString();
            }

            model.CookingTimeMinutes = totalTime.ToString();

            var recipes = await _apiService.CreateRecipe(model);

            var guid = _cookieProvider.GetGuidFromCookies(HttpContext);
            var auth = _authRepository.LoginnedByToken(guid);
            var user = _userRepository.GetById(auth.UserId);
            var recipesIds = user.RecipesIds?.ToList();
            recipesIds.Add(recipes.Id);
            user.RecipesIds = recipesIds;
            _userRepository.Update(user.Id, user);

            return Redirect("/Recipe/Recipes");
        }

        public async Task<IActionResult> Recipes()
        {
            var guid = _cookieProvider.GetGuidFromCookies(HttpContext);
            var auth = _authRepository.LoginnedByToken(guid);
            var user = _userRepository.GetById(auth.UserId);


            var recipes = await _apiService.GetManyRecipiesByIds(user.RecipesIds);

            return View(recipes);
        }

        public IActionResult Ingredient([FromQuery] string pointId)
        {
            ViewBag.PointId = pointId;
            return PartialView("_Ingredient");
        }

        public IActionResult Checkpoint()
        {
            return PartialView("_Checkpoint");
        }
    }
}
