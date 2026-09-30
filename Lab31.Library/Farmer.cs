namespace Lab31.Library;

public class Farmer : Person
{
    public string Farm { get; set; }

    public Farmer(string firstName, string lastName, string farm)
        : base(firstName, lastName)
    {
        Farm = farm;
    }

    public string Work() => $"{FirstName} {LastName} працює у господарстві.";
}
