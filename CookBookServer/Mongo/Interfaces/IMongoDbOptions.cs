namespace CookBookServer.Interfaces
{
    public interface IMongoDbOptions
    {

        string DataBaseName { get; set; }
        string CookAuthCollectionName { get; set; }
        string CollectionName { get; set; }

    }
}
