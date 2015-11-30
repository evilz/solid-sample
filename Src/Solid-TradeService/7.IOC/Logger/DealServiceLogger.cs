using System;
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
                _logger.Info(String.Format("Saving : {0}.", entity));
            else
                _logger.Warn(String.Format(" Try Saving : {0}.", entity));
            
        }

        public virtual void Saved(Maybe<T> deal)
        {
            if (deal.HasValue)
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

        public virtual void Loaded(T entity)
        {
            _logger.Info(String.Format("Returning deal {0}.", entity.Id));
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