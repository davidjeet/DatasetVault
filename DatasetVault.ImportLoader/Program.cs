using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetVault.AzureSearchRepository;
using DatasetVault.Common;
using DatasetVault.Common.Interfaces;
using System.Data.SqlClient;
using DatasetVault.Common.Models;
using System.Data;

namespace DatasetVault.ImportLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            ////VaultDB.Initialize();

            //0. Init
            IAzureSearchRepository repo = new AzureSearchService();

            //1. delete existing indexes
            repo.DeleteIndex();

            //2. create index
            repo.CreateIndex();

            //3. create index and loop thru a collection and insert
            repo.Populate(InsertRows());

            Console.WriteLine("Index populated");
            Console.ReadKey();
        }

        private static IEnumerable<DatasetEntry> InsertRows()
        {
            string connString = DVConfiguration.Instance.SourceSqlConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandText = "Select * from archive";
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DatasetEntry entry = new DatasetEntry
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                Notes = reader["Notes"].ToString(),
                                Category = reader["Category"].ToString(),
                                IsImported = Convert.ToBoolean(reader["IsImported"].ToString())
                            };
                            yield return entry;
                        }
                    }
                }
            }
        }
    }
}
