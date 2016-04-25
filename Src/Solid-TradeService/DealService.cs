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

        public DealService()
        {
            _cache = ImmutableDictionary<string, Deal>.Empty;
        }

        public string Username { get; set; }
        
        public string Save(string id, Deal deal)
        {
            _logger.Info("Saving deal "+ deal.Id);
            if (!Directory.Exists(Username))
            {
                Directory.CreateDirectory(Username);
            }
            var path = Path.Combine(Username, id + ".json");
            File.WriteAllText(path, JsonConvert.SerializeObject(deal));
            var savedDeal = ImmutableInterlocked.AddOrUpdate(ref _cache, id, deal, (i, d) => deal );
            _logger.Info("Saved deal " + savedDeal.Id );
            return path;
        }

        public Deal Load(string id)
        {
            _logger.Info("Loading deal " + id);
            var path = Path.Combine(Username, id + ".json");
            var deal = ImmutableInterlocked.GetOrAdd(ref _cache, id,
                _ => JsonConvert.DeserializeObject<Deal>(File.ReadAllText(path)));
            _logger.Info("Returning deal " + id);
            return deal;
        }
    }
}
