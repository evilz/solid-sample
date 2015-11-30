using TradeApp.Logging;
using TradeApp.Models;

namespace TradeApp
{
    public class DealServiceLogger
    {
        private static readonly ILog _logger = LogProvider.For<DealService>();

        public void Saving(Deal deal)
        {
            _logger.Info(string.Format("Saving deal {0}.", deal.Id));
        }

        public void Saved(Deal deal)
        {
            _logger.Info(string.Format(@"Saved deal {0}.", deal.Id));
        }

        public void Loading(string id)
        {
            _logger.Info(string.Format("Loading deal {0}.", id));
        }

        public void DidNotFind(string id)
        {
            _logger.Info(string.Format("No deal {0} found.", id));
        }

        public void Loaded(Deal deal)
        {
            _logger.Info(string.Format("Returning deal {0}.", deal.Id));
        }
    }
}