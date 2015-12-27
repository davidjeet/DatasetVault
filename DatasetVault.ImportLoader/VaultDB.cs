using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetVault.ImportLoader
{
    public class VaultDB
    {
        public static void Initialize()
        {
            SqlConnection connection = new SqlConnection(@"server=(localdb)\v12.0");
            using (connection)
            {
                connection.Open();
                string path = @"C:\Github\DatasetVault\DatasetVault.ImportLoader";
                string sql = string.Format(@"
                                CREATE DATABASE
                                    [Test]
                                ON PRIMARY (
                                   NAME=Test_data,
                                   FILENAME = '{0}\VaultDB.mdf'
                                )
                                LOG ON (
                                    NAME=Test_log,
                                    FILENAME = '{0}\VaultDB.ldf'
                                )",
                               path
                           );

                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}