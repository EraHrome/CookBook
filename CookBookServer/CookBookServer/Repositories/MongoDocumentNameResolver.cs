using CookBookServer.Models.Recipe;
using CookBookServer.Models.User;
using CookBookServer.Models;
using System;

namespace CookBookServer.Repositories
{
    public static class MongoDocumentNameResolver
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
