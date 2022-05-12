using Npgsql;
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
                "localhost", 5432, "postgres", "123", "Escola");
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private string sql;
        public NpgsqlDataReader reader;

        public MainWindowVM()
        {
            School = new School();
            StartConnection();
            StartCommands();
            Select();
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
                    sql = @"SELECT * FROM st_insert(:_name, :_grade)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_name", student.Name);
                    cmd.Parameters.AddWithValue("_grade", (int)student.Grade);
                    if((int)cmd.ExecuteScalar() == 1)
                    {
                        MessageBox.Show("Student added with success");
                    }
                    else
                    {
                        MessageBox.Show("Failed to add Student");
                    }
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
                    sql = @"SELECT * FROM st_delete(:_id)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", SelectedStudent.Id);
                    if((int)cmd.ExecuteScalar() == 1)
                    {
                        School.RemoveStudent(SelectedStudent);
                        MessageBox.Show("Student removed with success");
                    } else
                    {
                        MessageBox.Show("Failed to remove Student");
                    }
                    conn.Close();
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

                conn.Open();
                sql = @"SELECT * FROM st_update(:_id, :_name, :_grade)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", SelectedStudent.Id);
                cmd.Parameters.AddWithValue("_name", SelectedStudent.Name);
                cmd.Parameters.AddWithValue("_grade", (int)SelectedStudent.Grade);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    School.UpdateStudent(SelectedStudent, student.Name, student.Grade);
                    MessageBox.Show("Student updated with success");
                }
                else
                {
                    MessageBox.Show("Failed to update Student");
                }
                conn.Close();
            }, (object _) =>
            {
                if (SelectedStudent != null) return true;
                return false;
            });
        }

        public void StartConnection()
        {
            conn = new NpgsqlConnection(connstring);
        }

        public void Select()
        {
            try
            {
                conn.Open();
                sql = @"SELECT * FROM st_select()";
                cmd = new NpgsqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                School.Clear();
                while(reader.Read())
                {
                   int id = int.Parse(reader["_id"].ToString());
                   string name = reader["_name"].ToString();
                   Grade grade = (Grade)int.Parse(reader["_grade"].ToString());
                   Student student = new(id, name, grade);
                   School.AddStudent(student);
                }
                conn.Close();
                
            } catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error :" + ex.Message);
            }
        }
    }

}
