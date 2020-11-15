using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mongo.Models.Recipe
{
    public class CheckpointModel
    {
        public CheckpointModel()
        {
            Ingredients = new List<IngredientModel>();
        }

        public string CookingSeconds { get; set; }
        public string Description { get; set; }
        public string TimerSeconds { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; }

        [NotMapped]
        public int TimerMinutes { get; set; }
        [NotMapped]
        public int CookingMinutes { get; set; }
    }
}
