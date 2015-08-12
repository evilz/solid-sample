namespace TradeApp
{
    public interface IDealStorage
    {
        void WriteDeal(string id, string serializedDeal);
        string ReadDeal(string id);
    }
}