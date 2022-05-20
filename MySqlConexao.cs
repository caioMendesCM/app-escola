using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LUZ_TREINAMENTO
{
    public class MySqlConexao : IConexao
    {
        private string server;
        private int port;
        private string user; 
        private string password;
        private string database;
        private string connstring;
        private string sql;
        private MySqlCommand cmd;
        private MySqlDataReader reader;
        private MySqlConnection conn;

        public MySqlConexao()
        {
            server = "localhost";
            port = 9999;
            user = "root";
            password = "my-secret-pw";
            database = "escola";
            StartConnection();
        }

        public void StartConnection()
        {
            connstring = String.Format(
                "Server={0};Port={1};User Id={2};Password={3};Database={4}",
                server, port, user, password, database);
            conn = new();
            conn.ConnectionString = connstring;
            cmd = new();
            cmd.Connection = conn;
        }
        // expect a table name like "students", parameters like "(nameA, nameB)
        // and what you're trying to add like (Caio Mendes, 2)"
        public int AdicionarNaTabela(string table, string parameters, string items)
        {
            sql = String.Format($"INSERT INTO {table} {parameters} values {items}");
            OpenConnections(true);
            return (int)cmd.LastInsertedId;
        }
        // expect a table name like "students", changes like "st_name = 'Jonathan Sousa'"
        // and where like "st_id = 1"
        public void AtualizarNaTabela(string table, string changes, string where)
        {
            sql = String.Format($"UPDATE {table} SET {changes} WHERE {where}");
            OpenConnections(true);
        }
        // expect a table name like "students" and where like "st_id = 1"
        public void RemoverDaTabela(string table, string where)
        {
            sql = String.Format($"DELETE FROM {table} WHERE {where}");
            OpenConnections(true);
        }
        public DbDataReader ReceberTabela(string table)
        {
            sql = String.Format($"SELECT * FROM {table}");
            OpenConnections(false);
            return reader;
        }

        public void OpenConnections(Boolean selfClose)
        {
            conn.Open();
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            if (selfClose)
            {
                CloseConnections();
            }
        }
        public void CloseConnections()
        {
            conn.Close();
            reader.Close();
        }

        public string Server
        {
            get { return server; }
            set { server = value; }
        }
        public int Port 
        {
            get { return port; }
            set { port = value; }
        }
        public string User 
        {
            get { return user; }
            set { user = value; }
        }
        public string Password 
        {
            get { return password; }
            set { password = value; }
        }
        public string Database
        {
            get { return database; }
            set { database = value; }
        }


    }
}
