namespace Lab31.Library;

public class Painter : Person
{
    public string Style { get; set; }

    public Painter(string firstName, string lastName, string style)
        : base(firstName, lastName)
    {
        Style = style;
    }

    public string Paint() => $"{FirstName} {LastName} малює в стилі {Style}.";
}
