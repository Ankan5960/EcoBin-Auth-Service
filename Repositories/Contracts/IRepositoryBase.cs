using System.Data;

namespace Auth_Api.Repositories.Contracts;

public interface IRepositoryBase<T>
{
    Task<IEnumerable<T>> QueryAsync(string query, object? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    Task<T?> QueryFirstOrDefaultAsync(string query, object? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    Task<Guid> ExecuteScalarAsync(string query, object? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    Task ExecuteAsync(string query, object? parameters = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);
}