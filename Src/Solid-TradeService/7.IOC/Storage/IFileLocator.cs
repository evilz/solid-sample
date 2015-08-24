using System.IO;
using TradeApp.Models;

namespace TradeApp
{
    public interface IFileLocator<T> where T : IIdentifiable
    {
        FileInfo GetFileName(string id);
    }
}