using System;
using System.Collections.Immutable;
using System.IO;
using Newtonsoft.Json;
using TradeApp.Logging;
using TradeApp.Models;

namespace TradeApp
{
    public class DealService
    {
        private ImmutableDictionary<string,Deal> _cache;
        private static readonly ILog _logger = LogProvider.For<DealService>();

        public DealService(string username)
        {
            if(username == null)
                { throw new ArgumentNullException("username"); }

            if (!Directory.Exists(username))
                { Directory.CreateDirectory(username); }

            Username = username;
            _cache = ImmutableDictionary<string, Deal>.Empty;
        }

        public string Username { get; private set; }

        public void Save(string id, Deal deal)
        {
            _logger.Info("Saving deal "+ deal.Id);
            var path = GetFileName(id);
            File.WriteAllText(path, JsonConvert.SerializeObject(deal));
            var savedDeal = ImmutableInterlocked.AddOrUpdate(ref _cache, id, deal, (i, d) => deal );
            _logger.Info("Saved deal " + savedDeal.Id );
        }

        public Maybe<Deal> Load(string id)
        {
            _logger.Info("Loading deal " + id);
            var path = GetFileName(id);

            if (!File.Exists(path))
                { return new Maybe<Deal>(); }

            var deal = ImmutableInterlocked.GetOrAdd(ref _cache, id,
                _ => JsonConvert.DeserializeObject<Deal>(File.ReadAllText(path)));
            _logger.Info("Returning deal " + id);
            return new Maybe<Deal>(deal);
        }

        public string GetFileName(string id)
        {
            return Path.Combine(Username, id + ".json");
        }
    }
}
