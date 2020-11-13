using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Interfaces
{
    public interface IHasStringId
    {
        [BsonId]
        string Id { get; set; }
    }
}
