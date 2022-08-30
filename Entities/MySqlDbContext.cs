using System.Data;
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

        public async Task<IDbConnection> OpenConnection()
        {
            await using var connection = new MySqlConnection(ConnString);
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
