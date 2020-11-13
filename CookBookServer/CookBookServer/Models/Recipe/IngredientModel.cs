using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Models.Recipe
{
    public class IngredientModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Amount { get; set; }

    }
}
