namespace TradeApp
{
    public interface IDealStorage
    {
        void Save(string id, string serializedDeal);
        Maybe<string> Load(string id);
    }
}