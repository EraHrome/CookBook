using CookBookServer.Models.DTO.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Controllers
{
    public class AuthController : Controller
    {
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
            //todo. проверка и проставление куки
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public IActionResult SignIn(SignInDTOModel model)
        {
            //todo. проверка и проставление куки
            return Redirect("/Home/Index");
        }
    }
}
