using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUZ_TREINAMENTO
{
    public class MySqlConexao : IConexao
    {
        private string connstring = String.Format("Server={0};Port={1};" +
                "User Id={2};Password={3};Database={4}",
                "localhost", 9999, "root", "my-secret-pw", "escola");
        private MySqlCommand cmd;
        private MySqlDataReader reader;
        private String sql;
        private MySqlConnection conn;

        public MySqlConexao()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = connstring;
            cmd = new();
            cmd.Connection = conn;
        }
        public int AdicionarNaTabela(Student student)
        {
            sql = "INSERT INTO students (st_name, st_grade) values ('" + student.Name + "', " + (int)student.Grade + ")";
            conn.Open();
            cmd.CommandText = sql;
            cmd.ExecuteReader();
            conn.Close();
            return (int)cmd.LastInsertedId;
        }
        public void AtualizarNaTabela(Student student, Student selectedStudent)
        {
            sql = "UPDATE students SET " +
                  "st_name = '" + student.Name +
                  "', st_grade = " + (int)student.Grade +
                  " WHERE st_id = " + selectedStudent.Id;
            conn.Open();
            cmd.CommandText = sql;
            cmd.ExecuteReader();
            conn.Close();
        }
        public void RemoverDaTabela(Student student)
        {
            sql = "DELETE FROM students WHERE st_id = " + student.Id;
            conn.Open();
            cmd.CommandText = sql;
            cmd.ExecuteReader();
            conn.Close();
        }
        public ObservableCollection<Student> ReceberTabela()
        {
            ObservableCollection<Student> list = new();
            sql = "SELECT * FROM students";
            conn.Open();
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt16(0);
                string name = reader.GetString(1);
                Grade grade = (Grade)reader.GetInt16(2);
                Student student = new(id, name, grade);
                list.Add(student);
            }
            reader.Close();
            conn.Close();
            return list;
        }

    }
}
