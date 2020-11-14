using System.Collections.Generic;
using System;

namespace Mongo.Models.Recipe
{
    public class CheckpointModel
    {
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; }
    }
}
