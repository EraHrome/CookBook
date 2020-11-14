using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;
using Mongo.Models;
using Mongo;

namespace Mongo.Repositories
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
            _collection = _database.GetCollection<AuthModel>(options.Value.CollectionName);
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

        public void DeleteOneByUserId(AuthModel authorizationModel)
        {
            _collection.DeleteOne(x => x.UserId == authorizationModel.UserId);
        }

        public void DeleteOneByGuid(string guid)
        {
            _collection.DeleteOne(x => x.Guid == guid);
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
