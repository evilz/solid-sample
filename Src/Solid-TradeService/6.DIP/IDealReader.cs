using TradeApp.Models;

namespace TradeApp
{
    public interface IDealReader
    {
        Maybe<Deal> Load(string id);
    }
}
