using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Models.Recipe
{
    public class CheckpointModel
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public List<IngredientModel> Ingredients { get; set; }
    }
}
