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

        public virtual void WriteDeal(string id, string serializedDeal)
        {
            var path = GetFileName(id).FullName;
            File.WriteAllText(path, serializedDeal);
        }

        public virtual string ReadDeal(string id)
        {
            var path = GetFileName(id).FullName;
            return File.ReadAllText(path);
        }

        public FileInfo GetFileName(string id)
        {
            return new FileInfo(Path.Combine(_directory, id + ".json"));
        }
    }
}
