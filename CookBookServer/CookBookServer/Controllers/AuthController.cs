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
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInDTOModel model)
        {
            return View();
        }
    }
}
