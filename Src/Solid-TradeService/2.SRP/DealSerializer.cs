using Newtonsoft.Json;
using TradeApp.Models;

namespace TradeApp
{
    public class DealSerializer
    {
        public string SerializeDeal(Deal deal)
        {
           return JsonConvert.SerializeObject(deal); 
        }

        public Deal DeserializeDeal(string serializedDeal)
        {
            return JsonConvert.DeserializeObject<Deal>(serializedDeal);
        }
    }
}
