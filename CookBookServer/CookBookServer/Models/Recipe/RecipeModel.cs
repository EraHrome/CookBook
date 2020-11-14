using System.Collections.Generic;
using CookBookServer.Interfaces;
using System;

namespace CookBookServer.Models.Recipe
{
    public class RecipeModel : IHasStringId
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        //public string Category { get; set; }
        //public IEnumerable<string> TagsList { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime CookingTime { get; set; }
        public IEnumerable<CheckpointModel> Checkpoints { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; }
        public Dictionary<int, int> Raitings { get; set; }
    }
}
