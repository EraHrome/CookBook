using Microsoft.Extensions.Options;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq;
using Mongo.Models.Recipe;
using CookBookServer.Repositories;
using Mongo.Models;

namespace Mongo.Repositories
{
    public class RecipesRepository : MongoRepository<RecipeModel>
    {
        public RecipesRepository(IOptions<MongoDbOptions> options, IMongoClient client) : base(options, client)
        { }

        public RecipeModel GetByUid(string id)
            => _doc.Find(x => x.Id == id)?.FirstOrDefault();

        public IEnumerable<RecipeModel> GetManyByUid(string id)
            => _doc.Find(x => x.Id == id)?.ToEnumerable();

        public IEnumerable<RecipeModel> GetManyByIds(string[] ids)
            => _doc.Find(x => ids.Contains(x.Id))?.ToEnumerable();

        public void Add(RecipeModel recipeModel)
            => _doc.InsertOne(recipeModel);

    }
}
