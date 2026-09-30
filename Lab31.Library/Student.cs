namespace Lab31.Library;

public class Student : Person
{
    public int Course { get; set; }
    public string StudentTicket { get; set; }
    public DateTime BirthDate { get; set; }

    public Student(string firstName, string lastName, int course,
        string studentTicket, DateTime birthDate) : base(firstName, lastName)
    {
        if (!Validator.IsTicket(studentTicket))
            throw new ArgumentException("Некоректний студентський квиток.");

        Course = course;
        StudentTicket = studentTicket;
        BirthDate = birthDate;
    }

    public string Study() => $"{FirstName} {LastName} навчається.";
}
