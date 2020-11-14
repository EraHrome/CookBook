﻿using Microsoft.AspNetCore.Authorization;
using CookBookServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using CookBookServer.Providers;
using Mongo.Repositories;
using CookBookServer.Services;

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


            var recipes = _apiService.GetManyRecipiesByIds(user.RecipesIds);

            return View(recipes);
        }

    }
}
