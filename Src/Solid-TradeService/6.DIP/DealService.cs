using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using TradeApp.Models;

namespace TradeApp
{
    public class DealService
    {
        private readonly IDealWriter _writer;
        private readonly IDealReader _reader;

        public DealService(IDealWriter writer, IDealReader reader)
        {
            if (writer == null) { throw new ArgumentNullException(nameof(writer)); }
            if (reader == null) { throw new ArgumentNullException(nameof(reader)); }
            
            _writer = writer;
            _reader = reader;

        }
        
        public void Save(Maybe<Deal> deal)
        {
             _writer.Save(deal);
        }

        public Maybe<Deal> Load(string id)
        { 
            return _reader.Load(id);
        }
    }
}
