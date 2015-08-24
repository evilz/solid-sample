using System.ComponentModel;
using System.Linq;
using Functional.Maybe;
using TradeApp.Logging;
using TradeApp.Models;

namespace TradeApp
{
    public class Logger<T> : IReadWrite<T> where T : IIdentifiable
    {
        private readonly IReadWrite<T> _decoretee;

        private static readonly ILog _logger = LogProvider.For<DealService>();

        public Logger(IReadWrite<T> decoretee)
        {
            _decoretee = decoretee;
        }

        public virtual void Saving(Maybe<T> entity)
        {
            if(entity.HasValue)
                _logger.Info($"Saving : {entity}.");
            else
                _logger.Warn($" Try Saving : {entity}.");
            
        }

        public virtual void Saved(Maybe<T> deal)
        {
            if (deal.HasValue)
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

        public virtual void Loaded(T entity)
        {
            _logger.Info($"Returning deal {entity.Id}.");
        }

        public Maybe<T> Load(string id)
        {
            Loading(id);
            var entity = _decoretee.Load(id);
            if(entity.HasValue)
                Loaded(entity.Value);
            else
            {
                DidNotFind(id);
            }
            return entity;
        }

        public void Save(Maybe<T> entity)
        {
            Saving(entity);
            _decoretee.Save(entity);
            Saved(entity);
        }
    }
}