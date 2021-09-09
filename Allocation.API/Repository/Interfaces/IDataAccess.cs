using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Allocation.API.Repository.Interfaces
{
    public interface IDataAccess
    {
        void Dispose();
        Task DisposeAsync();
        void Execute(string command);
        void Execute(string command, params SqlParameter[] param);
        Task ExecuteAsync(string command);
        Task ExecuteAsync(string command, params SqlParameter[] param);
        void ExecuteNonQuery(string command);
        void ExecuteNonQuery(string command, params SqlParameter[] param);
        Task ExecuteNonQueryAsync(string command);
        Task ExecuteNonQueryAsync(string command, params SqlParameter[] param);
        void ExecuteStoredProcedure(string command);
        void ExecuteStoredProcedure(string command, params SqlParameter[] param);

        Task ExecuteStoredProcedureAsync(string command);
        Task ExecuteStoredProcedureAsync(string command, params SqlParameter[] param);
        T GetValue<T>(int index);
        T GetValue<T>(string name);
        bool IsNull(int index);
        bool IsNull(string name);
        Task<bool> IsNullAsync(int index);
        void Load(string command, CommandType commandType, DataTable table, params SqlParameter[] param);
        Task LoadAsync(string command, CommandType commandType, DataTable table, params SqlParameter[] param);
        bool Read();
        Task<bool> ReadAsync();
        void ResetParams();
        void SetTimeout(int timeout);
    }
}