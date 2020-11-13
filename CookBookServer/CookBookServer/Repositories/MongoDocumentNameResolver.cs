using CookBookServer.Models;
using CookBookServer.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            if (type == typeof(CookAuthorizationModel))
            {
                return "Auth";
            }

            return string.Empty;
        }
    }
}
