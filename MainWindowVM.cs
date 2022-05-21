using Npgsql;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace LUZ_TREINAMENTO
{
    public class MainWindowVM
    {
        public Student SelectedStudent { get; set; }
        public ObservableCollection<Student> StudentsList { get; set; }
        public ICommand AddStudent { get; private set; }
        public ICommand RemoveStudent { get; private set; }
        public ICommand UpdateStudent { get; private set; }
        private IConexao mysqlcon;
        //private NpgsqlConnection conn;
        //private NpgsqlCommand cmd;
        //public NpgsqlDataReader reader;

        public MainWindowVM()
        {
            StudentsList = new ObservableCollection<Student>();
            StartConnection();
            StartCommands();
            Select();
        }
        public void StartConnection()
        {
            mysqlcon = new MySqlConexao("localhost", 9999, "root", "my-secret-pw", "escola");
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
                    StudentsList.Add(student);
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
                    StudentsList.Remove(SelectedStudent);
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
                    SelectedStudent.Update(student.Name, student.Grade);
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
                using MySqlDataReader reader = (MySqlDataReader)mysqlcon.ExecuteQuery(
                    "SELECT * FROM students");
                while (reader.Read())
                {
                    int id = reader.GetInt16(0);
                    string name = reader.GetString(1);
                    Grade grade = (Grade)reader.GetInt16(2);
                    Student student = new(id, name, grade);
                    StudentsList.Add(student);
                }
                mysqlcon.DisposeConnection();
            }
            catch (Exception ex)
            {   
                MessageBox.Show("Error :" + ex.Message);
            }
        }
    }

}
