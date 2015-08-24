using System;
using System.Collections.Immutable;
using System.Linq;
using Functional.Maybe;
using TradeApp.Models;

namespace TradeApp
{
    public class Caching<T> : IReadWrite<T> where T : IIdentifiable
    {
        private readonly IReadWrite<T> _decoretee;
        private ImmutableDictionary<string, Maybe<T>> _cache;

        public Caching(IReadWrite<T> decoretee)
        {
            _decoretee = decoretee;

            _cache = ImmutableDictionary<string, Maybe<T>>.Empty;
        }

        public void Save(Maybe<T> entity)
        {
            _decoretee.Save(entity);
            if(entity.HasValue)
                ImmutableInterlocked.AddOrUpdate(ref _cache, entity.Value.Id, entity, (i, d) => entity);

        }

        public Maybe<T> Load(string id)
        {
            return ImmutableInterlocked.GetOrAdd(ref _cache, id, i => _decoretee.Load(i));
        }
    }
}
