using System;
using Newtonsoft.Json;
using TradeApp.Models;

namespace TradeApp
{
    public class DealSerializer : IDealSerializer
    {
        public string SerializeDeal(Deal deal)
        {
            return JsonConvert.SerializeObject(deal);
        }

        public Maybe<Deal> DeserializeDeal(string serializedDeal)
        {
            try
            {
                return new Maybe<Deal>(JsonConvert.DeserializeObject<Deal>(serializedDeal));
            }
            catch (Exception)
            {
                return new Maybe<Deal>();
            }
        }
    }
}
