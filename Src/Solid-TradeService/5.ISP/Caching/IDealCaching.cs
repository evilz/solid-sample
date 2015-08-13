using System;
using TradeApp.Models;

namespace TradeApp
{
    public interface IDealCaching
    {
        void Save(string id, Maybe<Deal> deal);
        Maybe<Deal> Load(string id, Func<string, Maybe<Deal>> dealFactory);
    }
}