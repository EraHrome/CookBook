using MongoDB.Bson.Serialization.Attributes;

namespace Mongo.Interfaces
{
    public interface IHasStringId
    {
        [BsonId]
        string Id { get; set; }
    }
}
