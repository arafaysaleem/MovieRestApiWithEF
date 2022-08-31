using System.Data;
using System.Runtime.Intrinsics.Arm;
using MySqlConnector;

namespace Entities
{
    public class MySqlDbContext
    {
        private string ConnString { get; init; }

        public MySqlDbContext(string connString)
        {
            this.ConnString = connString;
        }

        private async Task<MySqlConnection> OpenConnection(MySqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }
            return connection;
        }

        private async Task CloseConnection(MySqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }

        public async Task<R> Read<R>(string procedure, Func<MySqlDataReader, Task<R>> mapper)
        {
            MySqlConnection conn = new MySqlConnection(ConnString);
            try
            {
                // Create and Open connection
                conn = await OpenConnection(conn);

                // Prepare SQL
                var command = new MySqlCommand(procedure, conn);

                // Set type to procedure
                command.CommandType = CommandType.StoredProcedure;

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
                await CloseConnection(conn);
            }
        }

        public async Task<R> Read<R>(string procedure,
            IDictionary<string, object> paramDict,
            Func<MySqlDataReader, Task<R>> mapper)
        {
            MySqlConnection conn = new MySqlConnection(ConnString);
            try
            {
                // Create and Open connection
                conn = await OpenConnection(conn);

                // Prepare SQL
                var command = new MySqlCommand(procedure, conn);

                // Set type to procedure
                command.CommandType = CommandType.StoredProcedure;

                // Attach parameters
                foreach (var param in paramDict)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

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
                await CloseConnection(conn);
            }
        }

        public async Task<object?> ReadScalar(string procedure, IDictionary<string, object?> paramDict)
        {
            MySqlConnection conn = new MySqlConnection(ConnString);
            try
            {
                // Create and Open connection
                conn = await OpenConnection(conn);

                // Prepare SQL
                var command = new MySqlCommand(procedure, conn);

                // Set type to procedure
                command.CommandType = CommandType.StoredProcedure;

                // Attach parameters
                foreach (var param in paramDict)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

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
                await CloseConnection(conn);
            }
        }

        public async Task<R> Set<R>(string procedure,
            IDictionary<string, object> paramDict,
            Func<int, R> mapper)
        {
            MySqlConnection conn = new MySqlConnection(ConnString);
            try
            {
                // Create and Open connection
                conn = await OpenConnection(conn);

                // Prepare SQL
                var command = new MySqlCommand(procedure, conn);

                // Set type to procedure
                command.CommandType = CommandType.StoredProcedure;

                // Attach parameters
                foreach (var param in paramDict)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

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
                await CloseConnection(conn);
            }
        }
    }
}
