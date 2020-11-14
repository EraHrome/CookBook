using Microsoft.Extensions.Options;
using CookBookServer.Models;
using MongoDB.Driver;
using System.Linq;

namespace CookBookServer.Repositories
{
    public class AuthRepository
    {

        private IMongoDatabase _database;
        protected IMongoCollection<AuthModel> _collection;
        private readonly MongoDbOptions _options;

        public AuthRepository(IOptions<MongoDbOptions> options, IMongoClient mongoClient)
        {
            _options = options.Value;
            _database = mongoClient.GetDatabase(_options.DataBaseName);
            _collection = _database.GetCollection<AuthModel>(MongoDocumentNameResolver.GetMongoDocumentName<AuthModel>());
        }

        public void UpdateOne(AuthModel authorizationModel)
        {
            _collection.DeleteOne(x => x.UserId == authorizationModel.UserId);
            _collection.InsertOne(new AuthModel()
                {
                    UserId = authorizationModel.UserId,
                    Guid = authorizationModel.Guid
                }
            );
        }

        /// <summary>
        /// проверка на авторизацию по гуиду
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public AuthModel LoginnedByToken(string guid)
            => _collection.Find(m => m.Guid == guid)?.FirstOrDefault();

    }
}
