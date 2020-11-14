using CookBookServer.Interfaces;

namespace CookBookServer.Models.Options
{
    public class MongoRecipedDbOptions : IMongoDbOptions
    {

        public string DataBaseName { get; set; }
        public string CookAuthCollectionName { get; set; }
        public string CollectionName { get; set; }

    }
}
