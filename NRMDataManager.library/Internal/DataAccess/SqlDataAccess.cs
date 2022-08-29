using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDataManager.library.Internal.DataAccess
{
    internal class SqlDataAccess
    {
        public string GetConnecttionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storeProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnecttionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storeProcedure, parameters,
                    commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }
        public void SaveData<T>(string storeProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnecttionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection())
            {
                connection.Execute(storeProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
              
            }
        }
    }
}
