using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace TradeApp
{
    public class FileDealStorage : IFileLocator, IDealStorage
    {
        private readonly string _directory;

        public FileDealStorage(string directory)
        {
            _directory = directory;
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
    }
}
