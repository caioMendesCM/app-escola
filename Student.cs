namespace LUZ_TREINAMENTO
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Grade Grade { get; set; }

        public Student(string name, Grade grade)
        {
            this.Name = name;
            this.Grade = grade;
        }
        public Student(int id, string name, Grade grade)
        {
            this.Id = id;
            this.Name = name;
            this.Grade = grade;
        }
        public Student()
        {

        }
        public void Update(string? name = null, Grade? grade = null)
        {
            this.Name = name ?? this.Name;
            this.Grade = grade ?? this.Grade;
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
