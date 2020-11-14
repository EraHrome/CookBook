using CookBookServer.Interfaces;

namespace CookBookServer.Models.Options
{
    public class MongoUsersDbOptions : IMongoDbOptions
    {

        public string DataBaseName { get; set; }
        public string CookAuthCollectionName { get; set; }
        public string CollectionName { get; set; }

    }
}
