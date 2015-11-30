using System;
using TradeApp.Logging;
using TradeApp.Models;

namespace TradeApp
{
    public class DealServiceLogger
    {
        private static readonly ILog _logger = LogProvider.For<DealService>();

        public virtual void Saving(Deal deal)
        {
            _logger.Info(String.Format("Saving deal {0}.", deal.Id));
        }

        public virtual void Saved(Deal deal)
        {
            _logger.Info(String.Format("Saved deal {0}.", deal.Id));
        }

        public virtual void Loading(string id)
        {
            _logger.Info(String.Format("Loading deal {0}.", id));
        }

        public virtual void DidNotFind(string id)
        {
            _logger.Info(String.Format("No deal {0} found.", id));
        }

        public virtual void Loaded(Deal deal)
        {
            _logger.Info(String.Format("Returning deal {0}.", deal.Id));
        }
    }
}