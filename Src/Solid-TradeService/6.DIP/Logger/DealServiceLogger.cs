using System;
using System.Linq;
using TradeApp.Logging;
using TradeApp.Models;

namespace TradeApp
{
    public class DealServiceLogger : IDealReader, IDealWriter
    {
        private readonly IDealReader _reader;
        private readonly IDealWriter _writer;
        private static readonly ILog _logger = LogProvider.For<DealService>();

        public DealServiceLogger(IDealReader reader, IDealWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public virtual void Saving(Maybe<Deal> deal)
        {
            if(deal.Any())
                _logger.Info(String.Format("Saving : {0}.", deal));
            else
                _logger.Warn(String.Format(" Try Saving : {0}.", deal));
            
        }

        public virtual void Saved(Maybe<Deal> deal)
        {
            if (deal.Any())
                _logger.Info(String.Format("Saved : {0}.", deal));
            else
                _logger.Warn(String.Format(" Try Saved : {0}.", deal));
        }

        public virtual void Loading(string id)
        {
            _logger.Info(String.Format("Loading deal {0}.", id));
        }

        public virtual void DidNotFind(string id)
        {
            _logger.Warn(String.Format("No deal {0} found.", id));
        }

        public virtual void Loaded(Deal deal)
        {
            _logger.Info(String.Format("Returning deal {0}.", deal.Id));
        }

        public Maybe<Deal> Load(string id)
        {
            Loading(id);
            var deal = _reader.Load(id);
            if(deal.Any())
                Loaded(deal.Single());
            else
            {
                DidNotFind(id);
            }
            return deal;
        }

        public void Save(Maybe<Deal> deal)
        {
            Saving(deal);
            _writer.Save(deal);
            Saved(deal);
        }
    }
}