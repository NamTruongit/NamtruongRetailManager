using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SqlDataAccess> _logger;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public SqlDataAccess(IConfiguration config,ILogger<SqlDataAccess> logger )
        {
            _config = config;
            _logger = logger;
        }
        public string GetConnecttionString(string name)
        {

            return _config.GetConnectionString(name);
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
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storeProcedure, parameters,
                    commandType: CommandType.StoredProcedure);

            }
        }

        //Open connect/start transaction method
        //Load using the transaction mehtod
        //save using the transaction method
        //close connection / stop transaction method
        //dispose


        private bool IsClosed = false;


        public List<T> LoadDataInTransaction<T, U>(string storeProcedure, U parameters)
        {

            List<T> rows = _connection.Query<T>(storeProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();
            return rows;
        }

        public void SaveDataInTransaction<T>(string storeProcedure, T parameters)
        {
            _connection.Execute(storeProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: _transaction);
        }
        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnecttionString(connectionStringName);
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            IsClosed = false;
        }
        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
            IsClosed = true;
        }
        public void RollbackTransaction()
        {
            _transaction.Rollback();
            _connection?.Close();
            IsClosed = true;
        }

        public void Dispose()
        {
            if (IsClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Commit transaction failed in the dispoes method");
                }

            }
            _transaction = null;
            _connection = null;

        }
    }
}
