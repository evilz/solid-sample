using System;
using System.IO;
using TradeApp.Models;

namespace TradeApp
{
    public class DealService
    {
    
        public DealService(string username)
        {
            if(username == null)
                { throw new ArgumentNullException("username"); }

            if (!Directory.Exists(username))
                { Directory.CreateDirectory(username); }

            Username = username;
            DealCaching = new DealCaching();
            DealStorage = new DealStorage();
            DealSerializer = new DealSerializer();
            DealServiceLogger = new DealServiceLogger();
        }
        
        public virtual DealCaching DealCaching { get; private set; }
        public virtual DealStorage DealStorage { get; private set; }
        public virtual DealServiceLogger DealServiceLogger { get; private set; }
        public virtual DealSerializer DealSerializer { get; private set; }

        public string Username { get;private set; }


        public void Save(string id, Deal deal)
        {
            DealServiceLogger.Saving(deal);
            var file = DealStorage.GetFileName(id,Username);
            var serializedDeal = DealSerializer.SerializeDeal(deal);
            DealStorage.WriteDeal(file.FullName,serializedDeal);
            DealCaching.AddOrUpdate(id, deal);
            DealServiceLogger.Saved(deal);
        }

        public Maybe<Deal> Load(string id)
        {
            DealServiceLogger.Loading(id);
            var file = DealStorage.GetFileName(id,Username);

            if (!file.Exists)
            {
                DealServiceLogger.DidNotFind(id);
                return new Maybe<Deal>();
            }

            try
            {
                var deal = DealCaching.GetOrAdd(id, _ =>
                    DealSerializer.DeserializeDeal(DealStorage.ReadDeal(file.FullName)));

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
