using System;
using System.Linq;
using Newtonsoft.Json;
using TradeApp.Models;

namespace TradeApp
{
    public class DealSerializer : IDealSerializer
    {
        public virtual string SerializeDeal(Deal deal)
        {
           return JsonConvert.SerializeObject(deal); 
        }

        public virtual Maybe<Deal> DeserializeDeal(Maybe<string> serializedDeal)
        {
            if(!serializedDeal.Any()) return new Maybe<Deal>();

            try
            {
                return new Maybe<Deal>(JsonConvert.DeserializeObject<Deal>(serializedDeal.First()));
            }
            catch (Exception)
            {
                return new Maybe<Deal>();
            }
        }
    }
}
