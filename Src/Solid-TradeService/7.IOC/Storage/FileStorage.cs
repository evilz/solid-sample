using System;
using System.IO;
using System.Linq;
using Functional.Maybe;
using TradeApp.Models;

namespace TradeApp
{
    public class FileStorage<T> : IFileLocator<T>, IReadWrite<T> where T : IIdentifiable
    {
        private readonly string _directory;
        private readonly ISerializer<T> _serializer;

        public FileStorage(IUsersManager usersManager, ISerializer<T> serializer)
        {
            _directory = usersManager.GetCurrentUserName();
            _serializer = serializer;
        }

        public FileInfo GetFileName(string id)
        {
            return new FileInfo(Path.Combine(_directory, id + ".json"));
        }

        public Maybe<T> Load(string id)
        {
            try
            {
                var path = GetFileName(id).FullName;
                var text = File.ReadAllText(path);
                return _serializer.Deserialize(text);
            }
            catch (Exception)
            {
                return new Maybe<T>();
            }
        }

        public void Save(Maybe<T> entity)
        {
            if (entity.HasValue)
            {
                var path = GetFileName(entity.Value.Id).FullName;
                var serializedDeal = _serializer.Serialize(entity.Value);
                File.WriteAllText(path, serializedDeal);
            }
        }
    }
}
