using Functional.Maybe;
using TradeApp.Models;

namespace TradeApp
{
    public interface ISerializer<T> where T : IIdentifiable
    {
        string Serialize(T entity);
        Maybe<T> Deserialize(string serializedEntity);
    }
}