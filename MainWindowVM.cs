using Npgsql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static LUZ_TREINAMENTO.Escola;

namespace LUZ_TREINAMENTO
{
    public class MainWindowVM
    {
        public School School { get; set; }
        public Student SelectedStudent { get; set; }
        public ICommand AddStudent { get; private set; }
        public ICommand RemoveStudent { get; private set; }
        public ICommand UpdateStudent { get; private set; }
        public string connstring = String.Format("Server={0};Port={1};" + 
                "User Id={2};Password={3};Database={4}",
                "localhost", 9999, "root", "my-secret-pw", "escola");
        //private NpgsqlConnection conn;
        //private NpgsqlCommand cmd;
        //private string sql;
        //public NpgsqlDataReader reader;
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader reader;
        private string sql;

        public MainWindowVM()
        {
            School = new School();
            StartConnection();
            StartCommands();
            Select();
        }
        public void StartConnection()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = connstring;
        }
        public void StartCommands()
        {
            AddStudent = new RelayCommand((object _) =>
            {      
                Student student = new();

                CreateStudent createStudentPopUp = new();
                createStudentPopUp.DataContext = student;
                createStudentPopUp.ShowDialog();

                try
                {
                    conn.Open();
                    sql = "INSERT INTO students (st_name, st_grade) values ('" + student.Name + "', " + (int)student.Grade + ")";
                    cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.ExecuteReader();
                    conn.Close();
                    Select();
                }catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("ERROR: "+ex.Message);
                }
            });

            RemoveStudent = new RelayCommand((object _) =>
            {
                try
                {
                    conn.Open();
                    sql = "DELETE FROM students WHERE st_id = " + SelectedStudent.Id;
                    cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.ExecuteReader();
                    conn.Close();
                    School.RemoveStudent(SelectedStudent);
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("ERROR: " + ex.Message);
                } 
            },(object _) =>
            {
                if (SelectedStudent != null) return true;
                return false;
            });

            UpdateStudent = new RelayCommand((object _) =>
            {
                Student student = SelectedStudent;

                UpdateStudent updateStudentPopUp = new();
                updateStudentPopUp.DataContext = student;
                updateStudentPopUp.ShowDialog();
                try
                {
                    conn.Open();
                    sql = "UPDATE students SET " +
                        "st_name = '" + student.Name +
                        "', st_grade = " + (int) student.Grade +
                        " WHERE st_id = " + SelectedStudent.Id;
                    cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.ExecuteReader();
                    conn.Close();
                    School.UpdateStudent(SelectedStudent, student.Name, student.Grade);

                }catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }, (object _) =>
            {
                if (SelectedStudent != null) return true;
                return false;
            });
        }

        public void Select()
        {
            try
            {
                conn.Open();
                sql = "SELECT * FROM students";
                cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                School.Clear();
                while(reader.Read())
                {
                   int id = int.Parse(reader["st_id"].ToString());
                   string name = reader["st_name"].ToString();
                   Grade grade = (Grade)int.Parse(reader["st_grade"].ToString());
                   Student student = new(id, name, grade);
                   School.AddStudent(student);
                }
                reader.Close();
                conn.Close();
                
            } catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error :" + ex.Message);
            }
        }
    }

}
