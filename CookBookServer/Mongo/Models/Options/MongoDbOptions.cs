using CookBookServer.Interfaces;

namespace Mongo.Models
{
    public class MongoDbOptions : IMongoDbOptions
    {

        public string DataBaseName { get; set; }
        public string CookAuthCollectionName { get; set; }
        public string CollectionName { get; set; }

    }
}
