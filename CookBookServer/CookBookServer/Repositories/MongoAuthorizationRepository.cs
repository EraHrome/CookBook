using Microsoft.Extensions.Options;
using MyCodeServer.Models.Options;
using CookBookServer.Models;
using MongoDB.Driver;
using System.Linq;
using CookBookServer.Repositories;

namespace MyCodeServer.Repositories
{
    public class MongoAuthorizationRepository
    {

        private IMongoDatabase _database;
        protected IMongoCollection<CookAuthorizationModel> _collection;
        private readonly MongoDbOptions _options;

        public MongoAuthorizationRepository(IOptions<MongoDbOptions> options, IMongoClient mongoClient)
        {
            _options = options.Value;
            _database = mongoClient.GetDatabase(_options.DataBaseName);
            _collection = _database.GetCollection<CookAuthorizationModel>(MongoDocumentNameResolver.GetMongoDocumentName<CookAuthorizationModel>());
        }

        public void UpdateOne(CookAuthorizationModel authorizationModel)
        {
            _collection.DeleteOne(x => x.UserId == authorizationModel.UserId);
            _collection.InsertOne(new CookAuthorizationModel()
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
        public CookAuthorizationModel LoginnedByToken(string guid)
            => _collection.Find(m => m.Guid == guid)?.FirstOrDefault();

    }
}
