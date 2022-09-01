using System.Data;
using MySqlConnector;

namespace Entities
{
    public class MySqlDbContext
    {
        private string ConnString { get; init; }

        public MySqlDbContext(string connString)
        {
            ConnString = connString;
        }

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(ConnString);
        }

        public async Task<MySqlConnection> OpenConnection(MySqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }
            return connection;
        }

        public async Task CloseConnection(MySqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }
    }
}
