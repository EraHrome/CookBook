namespace CookBookServer.Interfaces
{
    public interface IMongoDbOptions
    {

        public string DataBaseName { get; set; }
        public string CookAuthCollectionName { get; set; }
        public string CollectionName { get; set; }

    }
}
