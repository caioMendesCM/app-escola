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
        //public NpgsqlDataReader reader;
        private IConexao mysqlcon;

        public MainWindowVM()
        {
            School = new School();
            StartConnection();
            StartCommands();
            Select();
        }
        public void StartConnection()
        {
            mysqlcon = new MySqlConexao();
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
                    student.Id = mysqlcon.AdicionarNaTabela(student);
                    School.AddStudent(student);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: "+ex.Message);
                }
            });

            RemoveStudent = new RelayCommand((object _) =>
            {
                try
                {
                    mysqlcon.RemoverDaTabela(SelectedStudent);
                    School.RemoveStudent(SelectedStudent);
                }
                catch (Exception ex)
                {
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
                    mysqlcon.AtualizarNaTabela(student, SelectedStudent);
                    School.UpdateStudent(SelectedStudent, student.Name, student.Grade);
                }
                catch (Exception ex)
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
                School.Students = mysqlcon.ReceberTabela();
            }
            catch (Exception ex)
            {   
                MessageBox.Show("Error :" + ex.Message);
            }
        }
    }

}
