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
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var recipes = await _apiService.GetRecipes();
                return View(recipes);
            }

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
            var user = _userRepository.GetById(auth.Id);
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
                    Raiting = 0.0,
                    CookingTimeMinutes = "30 минут",
                    Ingredients = new IngredientModel[]
                    {
                    new IngredientModel()
                            {
                             Id = Guid.NewGuid().ToString(),
                             Amount = "3 шт",
                             Title = "Мясо"
                            },
                    new IngredientModel()
                            {
                             Id = Guid.NewGuid().ToString(),
                             Amount = "1 шт",
                             Title = "Соус"
                            },
                    new IngredientModel()
                            {
                             Id = Guid.NewGuid().ToString(),
                             Amount = "200 мл",
                             Title = "Бульон"
                            },
                    },
                    Checkpoints = new CheckpointModel[]
                    {
                       new CheckpointModel ()
                       {
                       CookingSeconds = null,
                       Description = "Срежьте с куска мяса лишний жир, если он, по-вашему, есть. Разрежьте стейк на 2 равные части, сохраняя толщину. Растолките перец в ступке не очень мелко. Натрите мясо с двух сторон (не там, где разрез) солью, вдавите пальцами перец. Дайте мясу полежать 15–20 мин.",
                       TimerSeconds = "20",
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
                       Description = "В хорошо разогретой на сильном огне сковороде с толстым дном растопите в оливковом сливочное масло. Уложите стейки на сковороду, жарьте 3 мин. Переверните и жарьте еще 2 мин. Снимите сковороду с огня и дайте постоять 5 мин. Затем верните сковороду на огонь и жарьте еще по 1–3 мин. на каждой стороне, до желаемой степени прожарки. Переложите мясо на подогретые тарелки и накройте куском фольги, не заворачивая края.",
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
                       Description = "Пока мясо отдыхает, сделайте соус. Нарежьте очень мелко шалот, положите его на сковороду, где жарилось мясо. Обжарьте на среднем огне 2 мин. Наклоните сковороду на огне и, держась от нее подальше, влейте коньяк. Он должен загореться (если у вас электрическая плита или коньяк не загорелся от газа, подожгите его прямо в сковороде длинной спичкой). Дайте алкоголю прогореть.",
                       Ingredients = new IngredientModel[]
                       {
                           new IngredientModel()
                            {
                             Id = Guid.NewGuid().ToString(),
                             Amount = "1 шт",
                             Title = "Соус"
                            },

                        }
                     },
                       new CheckpointModel ()
                       {
                       CookingSeconds = null,
                       Description = "Добавьте теплый бульон, готовьте на сильном огне, помешивая, 1 мин. Добавьте масло, снимите с огня и подайте к стейкам.",
                           Ingredients = new IngredientModel[]
                       {
                           new IngredientModel()
                            {
                             Id = Guid.NewGuid().ToString(),
                             Amount = "200 мл",
                             Title = "теплый бульон"
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
