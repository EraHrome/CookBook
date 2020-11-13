using CookBookServer.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Controllers
{
    public class UserController : Controller
    {
        [Authentifaction]
        public IActionResult Index()
        {
            return View();
        }

        [Authentifaction]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
