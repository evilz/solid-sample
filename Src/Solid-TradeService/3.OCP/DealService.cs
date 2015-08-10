using System;
using System.IO;
using TradeApp.Models;

namespace TradeApp
{
    public class DealService
    {
        private readonly DealServiceLogger _dealServiceLogger;
        private readonly DealCaching _dealCaching;
        private readonly DealStorage _dealStorage;
        private readonly DealSerializer _dealSerializer;

        public DealService(string username)
        {
            if(username == null)
                { throw new ArgumentNullException(nameof(username)); }

            if (!Directory.Exists(username))
                { Directory.CreateDirectory(username); }

            Username = username;
            _dealCaching = new DealCaching();
            _dealStorage = new DealStorage();
            _dealSerializer = new DealSerializer();
            _dealServiceLogger = new DealServiceLogger();
        }
        
        public virtual DealCaching DealCaching => _dealCaching;

        public virtual DealStorage DealStorage => _dealStorage;

        public virtual DealServiceLogger DealServiceLogger => _dealServiceLogger;

        public virtual DealSerializer DealSerializer => _dealSerializer;

        public string Username { get; }


        public void Save(string id, Deal deal)
        {
            _dealServiceLogger.Saving(deal);
            var file = _dealStorage.GetFileName(id,Username);
            var serializedDeal = _dealSerializer.SerializeDeal(deal);
            _dealStorage.WriteDeal(file.FullName,serializedDeal);
            _dealCaching.AddOrUpdate(id, deal);
            _dealServiceLogger.Saved(deal);
        }

        public Maybe<Deal> Load(string id)
        {
            _dealServiceLogger.Loading(id);
            var file = _dealStorage.GetFileName(id,Username);

            if (!file.Exists)
            {
                _dealServiceLogger.DidNotFind(id);
                return new Maybe<Deal>();
            }

            try
            {
                var deal = _dealCaching.GetOrAdd(id, _ =>
                    _dealSerializer.DeserializeDeal(_dealStorage.ReadDeal(file.FullName)));

                _dealServiceLogger.Loaded(deal);
                return new Maybe<Deal>(deal);
            }
            catch (Exception)
            {
                return new Maybe<Deal>();
            }
        }
        

    }
}
