using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUZ_TREINAMENTO
{
    public class Student
    {
        private string name;
        private int id;
        private Grade grade;

        public Student(string name, int id, Grade grade)
        {
            this.name = name;
            this.id = id;
            this.grade = grade;
        }

        public Student() { }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Grade Grade
        {
            get { return grade; }
            set { grade = value; }
        }
    }

    public enum Grade
    {
        FirstGrade = 1,
        SecondGrade = 2,
        ThirdGrade = 3,
        FourthGrade = 4,
        FifthGrade = 5,
        SixthGrade = 6,
        SeventhGrade = 7,
        eighthGrade = 8,
        FirstYearHS = 9,
        SecondYearHS = 10,
        ThirdYearHS = 11,
    }
}
