using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using CookBookServer.Models;
using System.Diagnostics;
using CookBookServer.Providers;
using CookBookServer.Repositories;
using Mongo.Repositories;
using CookBookServer.Services;
using Mongo.Models.Recipe;
using System;
using System.Linq;

namespace CookBookServer.Controllers
{
    public class HomeController : Controller
    {

        public readonly CookieProvider _cookieProvider;
        public readonly UserRepository _userRepository;
        public readonly AuthRepository _authRepository;
        public readonly ApiService _apiService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, CookieProvider cookieProvider,
            UserRepository userRepository, AuthRepository authRepository, ApiService apiService)
        {
            _cookieProvider = cookieProvider;
            _userRepository = userRepository;
            _authRepository = authRepository;
            _apiService = apiService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return View();

            return View("Main");
        }

        public string AddTestRecept()
        {

            var guid = _cookieProvider.GetGuidFromCookies(HttpContext);
            if (String.IsNullOrEmpty(guid))
            {
                return "Вы не авторизированы";
            }
            var auth = _authRepository.LoginnedByToken(guid);
            if (auth == null)
            {
                return "Аут. Пользователь не найден";
            }
            var user = _userRepository.GetById(auth.UserId);
            if (user == null)
            {
                return "Пользователь не найден";
            }

            var recipes = new RecipeModel[]
            {
                new RecipeModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    PublicationDate = DateTime.Now,
                    Title = "Этап 1",
                    ImageName = "test",
                    Raiting = 4.0,
                    CookingTimeMinutes = "30 минут",
                    Checkpoints = new CheckpointModel[]
                    {
                       new CheckpointModel ()
                       {
                       CookingSeconds = null,
                       Description = "начните готовить",
                       Ingredients = new IngredientModel[]
                       {
                           new IngredientModel()
                            {
                             Id = Guid.NewGuid().ToString(),
                             Amount = "3 шт",
                             Title = "Мясо"
                            },

                        }
                     },
                       new CheckpointModel ()
                       {
                       CookingSeconds = "10",
                       Description = "начните готовить",
                       Ingredients = new IngredientModel[]
                       {
                           new IngredientModel()
                            {
                             Id = Guid.NewGuid().ToString(),
                             Amount = "3 шт",
                             Title = "Мясо"
                            },

                        }
                     },
                       new CheckpointModel ()
                       {
                       CookingSeconds = null,
                       Description = "начните готовить",
                       Ingredients = new IngredientModel[]
                       {
                           new IngredientModel()
                            {
                             Id = Guid.NewGuid().ToString(),
                             Amount = "1 шт",
                             Title = "Хлеб"
                            },

                        }
                     }
                 }
            }
            };

            foreach (var rec in recipes)
            {
                _apiService.CreateRecipe(rec);
            }

            user.RecipesIds = recipes.Select(x => x.Id);
            _userRepository.Update(user.Id, user);

            return "test data was added";
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
