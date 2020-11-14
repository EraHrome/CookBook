using CookBookServer.Interfaces;

namespace CookBookServer.Models
{
    public class MongoAuthorizedDbOptions : IMongoDbOptions
    {

        public string DataBaseName { get; set; }
        public string CookAuthCollectionName { get; set; }
        public string CollectionName { get; set; }

    }
}
