using TradeApp.Models;

namespace TradeApp
{
    public interface IDealServiceLogger
    {
        void Saving(Deal deal);
        void Saved(Deal deal);
        void Loading(string id);
        void DidNotFind(string id);
        void Loaded(Deal deal);
    }
}