using CookBookServer.Repositories.Interfaces;
using CookBookServer.Models.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using CookBookServer.Interfaces;
using CookBookServer.Models;
using MongoDB.Driver;
using System.Linq;

namespace CookBookServer.Repositories
{
    public class MongoRepository<T> : IMongoService<T> where T : class, IHasStringId
    {
        private IMongoDatabase _database;
        protected IMongoCollection<T> _doc;

        public MongoRepository(IOptions<MongoAuthorizedDbOptions> settings, IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase(settings.Value.DataBaseName);
            _doc = _database.GetCollection<T>(MongoDocumentNameResolver.GetMongoDocumentName<T>());
        }

        public MongoRepository(IOptions<MongoRecipedDbOptions> settings, IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase(settings.Value.DataBaseName);
            _doc = _database.GetCollection<T>(MongoDocumentNameResolver.GetMongoDocumentName<T>());
        }

        public MongoRepository(IOptions<MongoUsersDbOptions> settings, IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase(settings.Value.DataBaseName);
            _doc = _database.GetCollection<T>(MongoDocumentNameResolver.GetMongoDocumentName<T>());
        }


        public List<T> Get() =>
            _doc.Find(doc => true).ToList();

        public T Get(string id) =>
            _doc.Find(doc => doc.Id == id).FirstOrDefault();

        public T Create(T book)
        {
            _doc.InsertOne(book);
            return book;
        }

        public void Update(string id, T bookIn) =>
            _doc.ReplaceOne(doc => doc.Id == id, bookIn);


        public void Remove(T bookIn) =>
            _doc.DeleteOne(doc => doc.Id == bookIn.Id);

        public void Remove(string id) =>
            _doc.DeleteOne(doc => doc.Id == id);
    }
}
