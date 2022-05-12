using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUZ_TREINAMENTO
{
    public class School
    {
        public ObservableCollection<Student> Students { get; set; }

        public School()
        {
            Students = new ObservableCollection<Student>();
        }

        public void Clear()
        {
            Students.Clear();
        }

        public void AddStudent(string name, Grade grade)
        {
            this.Students.Add(new Student(name, grade));
        }

        public void AddStudent(Student student)
        {
            this.Students.Add(student);
        }

        public void UpdateStudent(Student student, string? nome = null, Grade? grade = null)
        {
            student.Name = nome ?? student.Name;
            student.Grade = grade ?? student.Grade;
        }

        public void RemoveStudent(Student student)
        {
            this.Students.Remove(student);
        }
    }
}
