using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;

namespace TradeApp
{
    public class SQLDealStorage : DealStorage
    {
        private readonly string _connectionString;

        public SQLDealStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override string ReadDeal(string path)
        {
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Query<string>("select Value from Deals where Id = @Id", new { Id = path });
                return result.First();
            }
        }

        public override void WriteDeal(string path, string serializedDeal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"delete from Deals where Id = @Id", new { Id = path });
                connection.Execute(@"insert Deals(Id, Value) values (@Id, @Value)",
                    new {Id = path, Value = serializedDeal});
            }
        }

        public override FileInfo GetFileName(string id, string username)
        {
            throw new NotImplementedException("Don't call this method if using SQL");
        }
    }
}
