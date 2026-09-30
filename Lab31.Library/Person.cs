namespace Lab31.Library;

public abstract class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    protected Person(string firstName, string lastName)
    {
        if (!Validator.IsName(firstName) || !Validator.IsName(lastName))
            throw new ArgumentException("Некоректне ім'я або прізвище.");

        FirstName = firstName;
        LastName = lastName;
    }

    public string Swim() => $"{FirstName} {LastName} вміє плавати.";
}
