using TradeApp.Logging;
using TradeApp.Models;

namespace TradeApp
{
    public class DealServiceLogger
    {
        private static readonly ILog _logger = LogProvider.For<DealService>();

        public void Saving(Deal deal)
        {
            _logger.Info($"Saving deal {deal.Id}.");
        }

        public void Saved(Deal deal)
        {
            _logger.Info($"Saved deal {deal.Id}.");
        }

        public void Loading(string id)
        {
            _logger.Info($"Loading deal {id}.");
        }

        public void DidNotFind(string id)
        {
            _logger.Info($"No deal {id} found.");
        }

        public void Loaded(Deal deal)
        {
            _logger.Info($"Returning deal {deal.Id}.");
        }
    }
}