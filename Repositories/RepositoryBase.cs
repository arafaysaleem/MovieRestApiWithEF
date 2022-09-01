using Contracts;
using Entities;
using MySqlConnector;
using System.Data;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MySqlDbContext _db;

        public RepositoryBase(MySqlDbContext db)
        {
            _db = db;
        }

        public async Task<R> ReadAsync<R>(string procedure, Func<MySqlDataReader, Task<R>> mapper)
        {
            var conn = _db.CreateConnection();
            try
            {
                // Prepare SQL
                var command = new MySqlCommand(procedure, conn)
                {
                    // Set type to procedure
                    CommandType = CommandType.StoredProcedure
                };

                // Create and Open connection
                conn = await _db.OpenConnection(conn);

                // Execute SQL
                var reader = await command.ExecuteReaderAsync();

                return await mapper(reader);
            }
            catch
            {
                throw;
            }
            finally
            {
                await _db.CloseConnection(conn);
            }
        }

        public async Task<R> ReadAsync<R>(string procedure,
            IDictionary<string, object?> paramDict,
            Func<MySqlDataReader, Task<R>> mapper)
        {
            var conn = _db.CreateConnection();
            try
            {
                // Prepare SQL
                var command = new MySqlCommand(procedure, conn)
                {
                    // Set type to procedure
                    CommandType = CommandType.StoredProcedure
                };

                // Attach parameters
                foreach (var param in paramDict)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                // Create and Open connection
                conn = await _db.OpenConnection(conn);

                // Execute SQL
                var reader = await command.ExecuteReaderAsync();

                return await mapper(reader);
            }
            catch
            {
                throw;
            }
            finally
            {
                await _db.CloseConnection(conn);
            }
        }

        public async Task<object?> ReadScalarAsync(string procedure, IDictionary<string, object?> paramDict)
        {
            var conn = _db.CreateConnection();
            try
            {
                // Prepare SQL
                var command = new MySqlCommand(procedure, conn)
                {
                    // Set type to procedure
                    CommandType = CommandType.StoredProcedure
                };

                // Attach parameters
                foreach (var param in paramDict)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                // Create and Open connection
                conn = await _db.OpenConnection(conn);

                // Execute SQL
                var result = await command.ExecuteScalarAsync();

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                await _db.CloseConnection(conn);
            }
        }

        public async Task<R> SetAsync<R>(string procedure,
            IDictionary<string, object?> paramDict,
            Func<int, R> mapper)
        {
            var conn = _db.CreateConnection();
            try
            {
                // Prepare SQL
                var command = new MySqlCommand(procedure, conn)
                {
                    // Set type to procedure
                    CommandType = CommandType.StoredProcedure
                };

                // Attach parameters
                foreach (var param in paramDict)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                // Create and Open connection
                conn = await _db.OpenConnection(conn);

                // Execute SQL
                var modified = await command.ExecuteNonQueryAsync();

                return mapper(modified);
            }
            catch
            {
                throw;
            }
            finally
            {
                await _db.CloseConnection(conn);
            }
        }

        public async Task<R> InsertAsync<R>(string procedure,
            IDictionary<string, object?> paramDict,
            Func<int, R> mapper)
        {
            var conn = _db.CreateConnection();
            try
            {
                // Prepare SQL
                var command = new MySqlCommand(procedure, conn)
                {
                    // Set type to procedure
                    CommandType = CommandType.StoredProcedure
                };

                // Attach parameters
                foreach (var param in paramDict)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                // Create and Open connection
                conn = await _db.OpenConnection(conn);

                // Execute SQL
                var newId = await command.ExecuteScalarAsync();

                return mapper(Convert.ToInt32(newId));
            }
            catch
            {
                throw;
            }
            finally
            {
                await _db.CloseConnection(conn);
            }
        }
    }
}