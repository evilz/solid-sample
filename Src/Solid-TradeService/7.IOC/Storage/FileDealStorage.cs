using System;
using System.IO;
using System.Linq;
using TradeApp.Models;

namespace TradeApp
{
    public class FileDealStorage : IFileLocator, IDealReader, IDealWriter
    {
        private readonly string _directory;
        private readonly IDealSerializer _serializer;

        public FileDealStorage(string directory, IDealSerializer serializer)
        {
            _directory = directory;
            _serializer = serializer;
        }

        public void Save(string id, string serializedDeal)
        {
            var path = GetFileName(id).FullName;
            File.WriteAllText(path, serializedDeal);
        }

        public Maybe<string> Load(string id)
        {
            try
            {
                var path = GetFileName(id).FullName;
                var text = File.ReadAllText(path);
                return new Maybe<string>(text);
            }
            catch (Exception)
            {
                return new Maybe<string>();
            }
        }

        public FileInfo GetFileName(string id)
        {
            return new FileInfo(Path.Combine(_directory, id + ".json"));
        }

        Maybe<Deal> IDealReader.Load(string id)
        {
            try
            {
                var path = GetFileName(id).FullName;
                var text = File.ReadAllText(path);
                return _serializer.DeserializeDeal(text);
            }
            catch (Exception)
            {
                return new Maybe<Deal>();
            }
        }

        public void Save(Maybe<Deal> deal)
        {
            if (deal.Any())
            {
                var value = deal.Single();
                var path = GetFileName(value.Id).FullName;
                var serializedDeal = _serializer.SerializeDeal(value);
                File.WriteAllText(path, serializedDeal);
            }
        }
    }
}
