﻿using CookBookServer.Models.DTO.Auth;
using CookBookServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CookBookServer.Controllers
{
    public class AuthController : Controller
    {
        public readonly UserRepository _userRepository;

        public AuthController(UserRepository userRepository) 
        {
            _userRepository = userRepository;
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
            //todo. проверка и проставление куки
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public IActionResult SignIn(SignInDTOModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userRepository.Get(model);

                }
                //todo. проверка и проставление куки


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
