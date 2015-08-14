using System;
using System.Collections.Immutable;
using System.Linq;
using TradeApp.Models;

namespace TradeApp
{
    public class DealCaching : IDealReader, IDealWriter
    {
        private readonly IDealReader _reader;
        private readonly IDealWriter _writer;
        private ImmutableDictionary<string, Maybe<Deal>> _cache;

        public DealCaching(IDealReader reader, IDealWriter writer)
        {
            _reader = reader;
            _writer = writer;
            _cache = ImmutableDictionary<string, Maybe<Deal>>.Empty;
        }

        public void Save(Maybe<Deal> deal)
        {
            _writer.Save(deal);
            ImmutableInterlocked.AddOrUpdate(ref _cache, deal.Single().Id, deal, (i, d) => deal);

        }

        public Maybe<Deal> Load(string id)
        {
            return ImmutableInterlocked.GetOrAdd(ref _cache, id, i => _reader.Load(i));
        }
    }
}
