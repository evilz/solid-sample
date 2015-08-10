using Newtonsoft.Json;
using TradeApp.Models;

namespace TradeApp
{
    public class DealSerializer
    {
        public virtual string SerializeDeal(Deal deal)
        {
           return JsonConvert.SerializeObject(deal); 
        }

        public virtual Deal DeserializeDeal(string serializedDeal)
        {
            return JsonConvert.DeserializeObject<Deal>(serializedDeal);
        }
    }
}
