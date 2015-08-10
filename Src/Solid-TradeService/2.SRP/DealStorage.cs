using System.IO;

namespace TradeApp
{
    public class DealStorage
    {
        public void WriteDeal(string path, string serializedDeal)
        {
            File.WriteAllText(path, serializedDeal);
        }

        public string ReadDeal(string path)
        {
           return File.ReadAllText(path);
        }

        public FileInfo GetFileName(string id, string username)
        {
            return new FileInfo(Path.Combine(username, id + ".json"));
        }
    }
}
