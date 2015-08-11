using TradeApp.Logging;
using TradeApp.Models;

namespace TradeApp
{
    public class DealServiceLogger
    {
        private static readonly ILog _logger = LogProvider.For<DealService>();

        public virtual void Saving(Deal deal)
        {
            _logger.Info($"Saving deal {deal.Id}.");
        }

        public virtual void Saved(Deal deal)
        {
            _logger.Info($"Saved deal {deal.Id}.");
        }

        public virtual void Loading(string id)
        {
            _logger.Info($"Loading deal {id}.");
        }

        public virtual void DidNotFind(string id)
        {
            _logger.Info($"No deal {id} found.");
        }

        public virtual void Loaded(Deal deal)
        {
            _logger.Info($"Returning deal {deal.Id}.");
        }
    }
}