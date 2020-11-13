using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Models.Recipe
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<string> TagsList { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime CookingTime { get; set; }
        public List<CheckpointModel> Checkpoints { get; set; }
        public List<IngredientModel> Ingredients { get; set; }
        public Dictionary<int, int> Raitings { get; set; }
    }
}
