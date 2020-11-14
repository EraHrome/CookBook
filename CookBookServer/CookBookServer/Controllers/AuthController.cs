using AutoMapper;
using CookBookServer.Models;
using CookBookServer.Models.DTO.Auth;
using CookBookServer.Models.User;
using CookBookServer.Providers;
using CookBookServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CookBookServer.Controllers
{
    public class AuthController : Controller
    {
        public readonly UserRepository _userRepository;
        public readonly AuthRepository _authRepository;
        public readonly CookieProvider _cookieProvider;
        private readonly IMapper _mapper;

        public AuthController(
            UserRepository userRepository, 
            AuthRepository authRepository,
            CookieProvider cookieProvider,
            IMapper mapper) 
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _cookieProvider = cookieProvider;
            _mapper = mapper;
        }

        public IActionResult Recover()
        {
            return View();
        }

        public IActionResult RecoverResult()
        {
            //todo. проверка емейла, отправка ссылки на востановление
            ViewBag.IsSuccess = true;
            return View();
        }

        public IActionResult SignUp()
        {            
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpDTOModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var user = _mapper.Map<User>(model);
                var newUser = _userRepository.Create(user);

                ViewBag.ShowResult = true;
                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Пользователь с такими данными не найден";
                return View();
            }
        }

        [HttpPost]
        public IActionResult SignIn(SignInDTOModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var user = _userRepository.Get(model);

                if (!user.IsConfirmed)
                {
                    ViewBag.Error = "Подтвердите почту! Сообщение было отправлено вам на почту.";
                    return View();
                }                

                var auth = new AuthModel
                {
                    UserId = user.Id,
                    Guid = Guid.NewGuid().ToString()
                };
                _cookieProvider.UpdateGuidInCookies(HttpContext, auth.Guid);
                _authRepository.UpdateOne(auth);

                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Пользователь с такими данными не найден";
                return View();
            }
           
        }
    }
}
