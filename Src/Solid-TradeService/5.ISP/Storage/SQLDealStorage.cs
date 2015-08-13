using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;

namespace TradeApp
{
    public class SQLDealStorage : IDealStorage
    {
        private readonly string _connectionString;

        public SQLDealStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Maybe<string> Load(string id)
        {
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Query<string>("select Value from Deals where Id = @Id", new { Id = id }).ToArray();
                if(result.Any())
                    return new Maybe<string>(result.First());

                return new Maybe<string>();
            }
        }

        public void Save(string id, string serializedDeal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"delete from Deals where Id = @Id", new { Id = id });
                connection.Execute(@"insert Deals(Id, Value) values (@Id, @Value)",
                    new {Id = id, Value = serializedDeal});
            }
        }
    }
}
