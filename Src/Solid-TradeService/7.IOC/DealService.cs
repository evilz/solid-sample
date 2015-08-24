using System;
using Functional.Maybe;
using TradeApp.Models;

namespace TradeApp
{
    public class DealService
    {
        private readonly IReadWrite<Deal> _dealReadWrite;

        public DealService(IReadWrite<Deal> dealReadWrite)
        {
            if (dealReadWrite == null) { throw new ArgumentNullException(nameof(dealReadWrite)); }
            
            _dealReadWrite = dealReadWrite;
        }
        
        public void Save(Maybe<Deal> deal)
        {
             _dealReadWrite.Save(deal);
        }

        public Maybe<Deal> Load(string id)
        { 
            return _dealReadWrite.Load(id);
        }
    }
}
