using System.Collections.Immutable;
using System.IO;
using Newtonsoft.Json;
using TradeApp.Logging;
using TradeApp.Models;

namespace TradeApp
{
    /// <summary>
    /// DealService
    /// </summary>
    public class DealService
    {
        private ImmutableDictionary<string,Deal> _cache;
        private static readonly ILog _logger = LogProvider.For<DealService>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DealService"/> class.
        /// </summary>
        public DealService()
        {
            _cache = ImmutableDictionary<string, Deal>.Empty;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Saves the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="deal">The deal.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Loads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
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
