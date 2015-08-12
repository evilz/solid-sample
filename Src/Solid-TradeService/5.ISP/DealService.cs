using System;
using System.IO;
using TradeApp.Models;

namespace TradeApp
{
    public class DealService
    {
        private readonly DealServiceLogger _dealServiceLogger;
        private readonly DealCaching _dealCaching;
        private readonly FileDealStorage _fileDealStorage;
        private readonly DealSerializer _dealSerializer;

        public DealService(string username)
        {
            if(username == null)
                { throw new ArgumentNullException(nameof(username)); }

            if (!Directory.Exists(username))
                { Directory.CreateDirectory(username); }

            Username = username;
            _dealCaching = new DealCaching();
            _fileDealStorage = new FileDealStorage(username);
            _dealSerializer = new DealSerializer();
            _dealServiceLogger = new DealServiceLogger();
        }
        
        public virtual DealCaching DealCaching => _dealCaching;

        public virtual FileDealStorage FileDealStorage => _fileDealStorage;

        public virtual DealServiceLogger DealServiceLogger => _dealServiceLogger;

        public virtual DealSerializer DealSerializer => _dealSerializer;

        public virtual IFileLocator FileLocator => _fileDealStorage;

        public string Username { get; }


        public void Save(string id, Deal deal)
        {
            _dealServiceLogger.Saving(deal);
            var file = FileLocator.GetFileName(id);
            var serializedDeal = DealSerializer.SerializeDeal(deal);
            FileDealStorage.WriteDeal(file.FullName,serializedDeal);
            DealCaching.AddOrUpdate(id, deal);
            DealServiceLogger.Saved(deal);
        }

        public Maybe<Deal> Load(string id)
        {
            DealServiceLogger.Loading(id);
            var file = FileLocator.GetFileName(id);

            if (!file.Exists)
            {
                DealServiceLogger.DidNotFind(id);
                return new Maybe<Deal>();
            }

            try
            {
                var deal = DealCaching.GetOrAdd(id, _ =>
                    DealSerializer.DeserializeDeal(_fileDealStorage.ReadDeal(file.FullName)));

                DealServiceLogger.Loaded(deal);
                return new Maybe<Deal>(deal);
            }
            catch (Exception)
            {
                return new Maybe<Deal>();
            }
        }
        

    }
}
