using System.Globalization;

namespace Lab31.Library;

public class FileManager
{
    public void Save(string fileName, Person[] people, int count)
    {
        using StreamWriter writer = new(fileName);

        for (int i = 0; i < count; i++)
        {
            Person p = people[i];

            if (p is Student s)
            {
                writer.WriteLine($"Student {s.StudentTicket}");
                writer.WriteLine("{");
                Write(writer, "firstname", s.FirstName);
                Write(writer, "lastname", s.LastName);
                Write(writer, "course", s.Course.ToString());
                Write(writer, "studentTicket", s.StudentTicket);
                Write(writer, "birthDate", s.BirthDate.ToString("dd.MM.yyyy"));
                writer.WriteLine("};");
            }
            else if (p is Painter a)
            {
                writer.WriteLine("Painter Painter");
                writer.WriteLine("{");
                Write(writer, "firstname", a.FirstName);
                Write(writer, "lastname", a.LastName);
                Write(writer, "style", a.Style);
                writer.WriteLine("};");
            }
            else if (p is Farmer f)
            {
                writer.WriteLine("Farmer Farmer");
                writer.WriteLine("{");
                Write(writer, "firstname", f.FirstName);
                Write(writer, "lastname", f.LastName);
                Write(writer, "farm", f.Farm);
                writer.WriteLine("};");
            }
        }
    }

    public int Load(string fileName, Person[] people)
    {
        if (!File.Exists(fileName)) return 0;

        int count = 0;
        using StreamReader reader = new(fileName);

        while (!reader.EndOfStream && count < people.Length)
        {
            string? header = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(header)) continue;

            string type = header.Split(' ')[0];
            reader.ReadLine(); // {

            string firstName = Read(reader);
            string lastName = Read(reader);

            if (type == "Student")
            {
                int course = int.Parse(Read(reader));
                string ticket = Read(reader);
                DateTime birthDate = DateTime.ParseExact(
                    Read(reader), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                reader.ReadLine(); // };

                people[count++] = new Student(firstName, lastName, course, ticket, birthDate);
            }
            else if (type == "Painter")
            {
                string style = Read(reader);
                reader.ReadLine();
                people[count++] = new Painter(firstName, lastName, style);
            }
            else if (type == "Farmer")
            {
                string farm = Read(reader);
                reader.ReadLine();
                people[count++] = new Farmer(firstName, lastName, farm);
            }
        }

        return count;
    }

    public int FindByLastName(Person[] people, int count, string lastName)
    {
        for (int i = 0; i < count; i++)
            if (people[i].LastName == lastName) return i;
        return -1;
    }

    public int FindByTicket(Person[] people, int count, string ticket)
    {
        for (int i = 0; i < count; i++)
            if (people[i] is Student s && s.StudentTicket == ticket) return i;
        return -1;
    }

    public int DeleteByTicket(Person[] people, int count, string ticket)
    {
        int index = FindByTicket(people, count, ticket);
        if (index == -1) return count;

        // Після видалення зсуваємо всі наступні елементи на одну позицію вліво.
        for (int i = index; i < count - 1; i++)
            people[i] = people[i + 1];

        people[count - 1] = null!;
        return count - 1;
    }

    private static void Write(StreamWriter writer, string name, string value) =>
        writer.WriteLine($"\"{name}\": \"{value}\",");

    // У файлі кожен атрибут записаний як "назва": "значення", тому тут дістаємо лише значення.
    private static string Read(StreamReader reader)
    {
        string line = reader.ReadLine()!.Trim().TrimEnd(',');
        int first = line.IndexOf('"') + 1;
        int second = line.IndexOf('"', first);
        int third = line.IndexOf('"', second + 1) + 1;
        int fourth = line.IndexOf('"', third);
        return line[third..fourth];
    }
}
