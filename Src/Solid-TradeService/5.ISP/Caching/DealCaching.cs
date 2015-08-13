using System;
using System.Collections.Immutable;
using TradeApp.Models;

namespace TradeApp
{
    public class DealCaching : IDealCaching
    {
        private ImmutableDictionary<string, Maybe<Deal>> _cache;

        public DealCaching()
        {
            _cache = ImmutableDictionary<string, Maybe<Deal>>.Empty;
        }
        public virtual void Save(string id, Maybe<Deal> deal)
        {
                ImmutableInterlocked.AddOrUpdate(ref _cache, id, deal, (i, d) => deal );
        }

        public virtual Maybe<Deal> Load(string id, Func<string, Maybe<Deal>> dealFactory )
        {
            return ImmutableInterlocked.GetOrAdd(ref _cache, id, dealFactory);
        }
    }
}
