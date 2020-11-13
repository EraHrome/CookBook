using Microsoft.Extensions.Options;
using MyCodeServer.Models.Options;
using CookBookServer.Models;
using MongoDB.Driver;
using System.Linq;
using CookBookServer.Models.User;
using System.Threading.Tasks;
using System;
using MongoDB.Bson;
using CookBookServer.Repositories;

namespace MyCodeServer.Repositories
{
    public class UserRepository : MongoRepository<User>
    {
        public UserRepository(IOptions<MongoDbOptions> options, IMongoClient client) : base(options, client)
        {
           
        }
    }
}
