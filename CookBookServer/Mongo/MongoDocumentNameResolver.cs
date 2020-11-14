using Mongo.Models.Recipe;
using Mongo.Models;
using System;

namespace Mongo
{
    public class MongoDocumentNameResolver
    {
        public static string GetMongoDocumentName<T>() where T : class
        {
            Type type = typeof(T);
            if (type == typeof(User))
            {
                return "Users";
            }
            if (type == typeof(AuthModel))
            {
                return "Auth";
            }
            if(type == typeof(RecipeModel))
            {
                return "Recipe";
            }

            return string.Empty;
        }
    }
}
