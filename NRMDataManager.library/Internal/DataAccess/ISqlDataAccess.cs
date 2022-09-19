using System.Collections.Generic;

namespace NRMDataManager.library.Internal.DataAccess
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        void Dispose();
        string GetConnecttionString(string name);
        List<T> LoadData<T, U>(string storeProcedure, U parameters, string connectionStringName);
        List<T> LoadDataInTransaction<T, U>(string storeProcedure, U parameters);
        void RollbackTransaction();
        void SaveData<T>(string storeProcedure, T parameters, string connectionStringName);
        void SaveDataInTransaction<T>(string storeProcedure, T parameters);
        void StartTransaction(string connectionStringName);
    }
}