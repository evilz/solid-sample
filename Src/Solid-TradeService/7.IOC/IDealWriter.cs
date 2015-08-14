using TradeApp.Models;

namespace TradeApp
{
    public interface IDealWriter
    {
        void Save(Maybe<Deal> deal);
    }
}
