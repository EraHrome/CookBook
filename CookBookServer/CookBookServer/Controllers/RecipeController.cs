using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Controllers
{
    public class RecipeController : Controller
    {
        // GET: RecipeController
        public ActionResult Index()
        {
            return View();
        }
    }
}
