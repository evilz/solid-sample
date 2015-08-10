using System;
using System.Collections.Immutable;
using TradeApp.Models;

namespace TradeApp
{
    public class DealCaching
    {
        private ImmutableDictionary<string, Deal> _cache;

        public DealCaching()
        {
            _cache = ImmutableDictionary<string, Deal>.Empty;
        }
        public void AddOrUpdate(string id, Deal deal)
        {
            ImmutableInterlocked.AddOrUpdate(ref _cache, id, deal, (i, d) => deal);
        }

        public Deal GetOrAdd(string id, Func<string, Deal> dealFactory )
        {
            return ImmutableInterlocked.GetOrAdd(ref _cache, id, dealFactory);
        }
    }
}
