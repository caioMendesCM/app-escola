using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace LUZ_TREINAMENTO
{
    public class MySqlConexao : IConexao
    {
        private readonly string connstring;
        private MySqlConnection conn;

        public MySqlConexao(string server, int port, string user, string password, string database)
        {
            connstring = 
                $"Server={server};" +
                $"Port={port};" +
                $"User Id={user};" +
                $"Password={password};" +
                $"Database={database}";
        }
        public void ExecuteNonQuery(string query)
        {
            using (conn = new(connstring))
            {
                using (MySqlCommand cmd = new(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }
        public DbDataReader ExecuteQuery(string query)
        {
            conn = new(connstring);
            using (MySqlCommand cmd = new(query, conn))
            {
                conn.Open();
                return cmd.ExecuteReader();
            }
        }
        public void DisposeConnection()
        {
            conn?.Dispose();
        }
    }
}
