using System;
using Functional.Maybe;
using Newtonsoft.Json;
using TradeApp.Models;

namespace TradeApp
{
    public class JsonSerializer<T> : ISerializer<T> where T : IIdentifiable
    {
        public string Serialize(T entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        public Maybe<T> Deserialize(string serializedEntity)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(serializedEntity).ToMaybe();
            }
            catch (Exception)
            {
                return Maybe<T>.Nothing;
            }
        }
    }
}
