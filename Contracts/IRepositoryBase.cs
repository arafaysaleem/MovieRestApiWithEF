using MySqlConnector;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<R> ReadAsync<R>(string procedure, Func<MySqlDataReader, Task<R>> mapper);
        Task<R> ReadAsync<R>(string procedure, IDictionary<string, object?> paramDict, Func<MySqlDataReader, Task<R>> mapper);
        Task<object?> ReadScalarAsync(string procedure, IDictionary<string, object?> paramDict);
        Task<R> SetAsync<R>(string procedure, IDictionary<string, object?> paramDict, Func<int, R> mapper);
    }
}