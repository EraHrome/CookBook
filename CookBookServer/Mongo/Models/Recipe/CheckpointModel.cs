using System.Collections.Generic;

namespace Mongo.Models.Recipe
{
    public class CheckpointModel
    {
        public string CookingSeconds { get; set; }
        public string Description { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; }
    }
}
