using Microsoft.Extensions.Options;
using CookBookServer.Models;
using MongoDB.Driver;
using System.Linq;
using CookBookServer.Models.User;
using System.Threading.Tasks;
using System;
using MongoDB.Bson;
using CookBookServer.Repositories;
using CookBookServer.Models.DTO.Auth;

namespace CookBookServer.Repositories
{
    public class UserRepository : MongoRepository<User>
    {
        public UserRepository(IOptions<MongoAuthorizedDbOptions> options, IMongoClient client) : base(options, client)
        {
           
        }

        public User GetById(string id)
            => _doc.Find(x => x.Id == id)?.FirstOrDefault();

        public User Get(SignInDTOModel model)
        {
            var user = _doc.Find(x => 
            x.Login == model.Login
            &&
            x.Password == model.Password);

            if (user == null || !user.Any())
                throw new Exception("User not found!");

            return user.First();
        }

        public User GetByEmailOrLogin(string login, string email)
        {
            var user = _doc.Find(x =>
            x.Login == login
            ||
            x.Email == email);

            if (user == null || !user.Any())
                return null;

            return user.First();
        }
    }
}
