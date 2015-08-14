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
                _logger.Info($"Saving : {deal}.");
            else
                _logger.Warn($" Try Saving : {deal}.");
            
        }

        public virtual void Saved(Maybe<Deal> deal)
        {
            if (deal.Any())
                _logger.Info($"Saved : {deal}.");
            else
                _logger.Warn($" Try Saved : {deal}.");
        }

        public virtual void Loading(string id)
        {
            _logger.Info($"Loading deal {id}.");
        }

        public virtual void DidNotFind(string id)
        {
            _logger.Warn($"No deal {id} found.");
        }

        public virtual void Loaded(Deal deal)
        {
            _logger.Info($"Returning deal {deal.Id}.");
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