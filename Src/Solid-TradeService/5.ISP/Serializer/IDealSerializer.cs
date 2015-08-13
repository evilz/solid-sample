using TradeApp.Models;

namespace TradeApp
{
    public interface IDealSerializer
    {
        string SerializeDeal(Deal deal);
        Maybe<Deal> DeserializeDeal(Maybe<string> serializedDeal);
    }
}