using System;
using System.Collections.Generic;
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

        public MainWindowVM()
        {
            School = new School();
            StartCommands();
        }
        
        public void StartCommands()
        {
            AddStudent = new RelayCommand((object _) =>
            {      
                Student student = new();

                CreateStudent createStudentPopUp = new();
                createStudentPopUp.DataContext = student;
                createStudentPopUp.ShowDialog();

                School.AddStudent(student);
            });

            RemoveStudent = new RelayCommand((object _) =>
            {
                School.RemoveStudent(SelectedStudent);
            });

            UpdateStudent = new RelayCommand((object _) =>
            {
                Student student = SelectedStudent;

                UpdateStudent updateStudentPopUp = new();
                updateStudentPopUp.DataContext = student;
                updateStudentPopUp.ShowDialog();

                School.UpdateStudent(SelectedStudent, student.Name, student.Grade);
            }, (object _) =>
            {
                if (SelectedStudent != null) return true;
                return false;
            });
        }
    }

}
