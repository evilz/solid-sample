using System.IO;

namespace TradeApp
{
    public interface IFileLocator
    {
        FileInfo GetFileName(string id);
    }
}