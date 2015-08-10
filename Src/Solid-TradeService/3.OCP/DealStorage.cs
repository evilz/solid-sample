using System.IO;

namespace TradeApp
{
    public class DealStorage
    {
        public virtual void WriteDeal(string path, string serializedDeal)
        {
            File.WriteAllText(path, serializedDeal);
        }

        public virtual string ReadDeal(string path)
        {
           return File.ReadAllText(path);
        }

        public virtual FileInfo GetFileName(string id, string username)
        {
            return new FileInfo(Path.Combine(username, id + ".json"));
        }
    }
}
