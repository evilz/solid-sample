using System;
using System.IO;
using TradeApp.Models;

namespace TradeApp
{
    public class DealService
    {
        private readonly DealServiceLogger _dealServiceLogger = new DealServiceLogger();
        private readonly DealCaching _dealCaching = new DealCaching();
        private readonly DealStorage _dealStorage = new DealStorage();
        private readonly DealSerializer _dealSerializer = new DealSerializer();

        public DealService(string username)
        {
            if(username == null)
                { throw new ArgumentNullException("username"); }

            if (!Directory.Exists(username))
                { Directory.CreateDirectory(username); }

            Username = username;
        }

        public string Username { get;private set; }
        
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
