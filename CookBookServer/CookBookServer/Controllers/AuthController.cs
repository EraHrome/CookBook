using AutoMapper;
using CookBookServer.Providers;
using CookBookServer.Repositories;
using DTOModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Mongo.Models;
using Mongo.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public IActionResult Confirm([FromRoute] string Id)
        {
            var user = _userRepository.Get(Id);
            user.IsConfirmed = true;
            _userRepository.Update(Id, user);

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

        public IActionResult Complete()
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

                var oldUser = _userRepository.GetByEmailOrLogin(model.Login.Trim(), model.Email.Trim());
                if (oldUser != null)
                {
                    if (oldUser.Email == model.Email)
                        ViewBag.Error = "Емейл уже используется!";
                    if (oldUser.Login == model.Login)
                        ViewBag.Error = "Такой Логин уже используется!";

                    return View();
                }
                var user = _mapper.Map<User>(model);
                var newUser = _userRepository.Create(user);

                var auth = new AuthModel
                {
                    UserId = user.Id,
                    Guid = Guid.NewGuid().ToString()
                };
                _authRepository.UpdateOne(auth);

                //try
                //{
                //    var url = Url.Action("Confirm", "Auth", new { id = user.Id }, protocol: Request.Scheme);
                //    EmailService.Send(new Email
                //    {
                //        Subject = "Подтверждение регистрации",
                //        Recipients = new List<string> { user.Email },
                //        Body = $"Здравствуйте, {user.FirstName}!<br><br> Перейдите по ссылке, для того, чтобы завершить регистрацию!<br> {url}",
                //        IsBodyHtml = true
                //    });
                //}
                //catch (Exception ex)
                //{
                //    _userRepository.Remove(user);
                //    _authRepository.DeleteOne(auth);

                //    ViewBag.Error = "Не удалось отправить сообщение для подтверждения! Попробуйье позднее";
                //    return View();
                //}


                ViewBag.ShowResult = true;
                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Что-то пошло не так!";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDTOModel model)
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
                await Authenticate(String.Format("{0} {1}", user.FirstName, user.LastName));

                return Redirect("/Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Пользователь с такими данными не найден";
                return View();
            }
        }
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
                };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public IActionResult SignOut()
        {
            var guid = _cookieProvider.GetGuidFromCookies(HttpContext);
            _cookieProvider.DeleteGuidFromCookies(HttpContext);
            _authRepository.DeleteOneByGuid(guid);
            return View("SignIn");
        }

    }
}
