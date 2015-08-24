using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Functional.Maybe;
using TradeApp.Models;

namespace TradeApp
{
    public class SQLDealStorage<T> : IReadWrite<T> where T : IIdentifiable
    {
        private readonly string _connectionString;
        private readonly ISerializer<T> _serializer;

        public SQLDealStorage(string connectionString, ISerializer<T> serializer)
        {
            _connectionString = connectionString;
            _serializer = serializer;
        }
        
        public Maybe<T> Load(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Query<string>("select Value from Deals where Id = @Id", new { Id = id }).ToArray();
                return result.Any() 
                    ? _serializer.Deserialize(result.First()) 
                    : Maybe<T>.Nothing;
            }
        }

        public void Save(Maybe<T> entity)
        {
            if(!entity.HasValue) return;

            using (var connection = new SqlConnection(_connectionString))
            {
                var serializedDeal = _serializer.Serialize(entity.Value);

                connection.Open();
                connection.Execute(@"delete from Deals where Id = @Id", new { Id = entity.Value.Id });
                connection.Execute(@"insert Deals(Id, Value) values (@Id, @Value)",
                    new {entity.Value.Id, Value = serializedDeal });
            }
        }
    }
}
