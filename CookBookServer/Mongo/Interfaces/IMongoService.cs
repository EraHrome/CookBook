using System.Collections.Generic;

namespace Mongo.Interfaces
{
    public interface IMongoService<T>
    {
        List<T> Get();
        T Get(string id);
        T Create(T book);
        void Update(string id, T bookIn);
        void Remove(T bookIn);
        void Remove(string id);
    }
}

