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
            _collection = _database.GetCollection<AuthModel>(MongoDocumentNameResolver.GetMongoDocumentName<AuthModel>());
        }

        public void UpdateOne(AuthModel authorizationModel)
        {
            _collection.DeleteOne(x => x.Id == authorizationModel.Id);
            _collection.InsertOne(new AuthModel()
            {
                Id = authorizationModel.Id,
                Guid = authorizationModel.Guid
            }
            );
        }

        public void DeleteOneByUserId(AuthModel authorizationModel)
        {
            _collection.DeleteOne(x => x.Id == authorizationModel.Id);
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
