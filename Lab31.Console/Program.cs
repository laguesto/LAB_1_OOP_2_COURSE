using Lab31.Library;

namespace Lab31.ConsoleApp;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        string fileName = "data.txt";
        Person[] people = new Person[50];
        FileManager files = new FileManager();
        int count = files.Load(fileName, people);

        // Якщо файл ще не створювали, додаємо стартові записи для демонстрації роботи програми.
        if (count == 0)
        {
            people[count++] = new Student("Іван", "Петренко", 2, "ST-123456", new DateTime(2006, 1, 15));
            people[count++] = new Student("Дмитро", "Романюк", 2, "ST-123457", new DateTime(2006, 12, 20));
            people[count++] = new Student("Олег", "Шевченко", 3, "ST-123458", new DateTime(2005, 3, 10));
            people[count++] = new Student("Анна", "Коваль", 1, "ST-123459", new DateTime(2007, 7, 8));
            people[count++] = new Student("Максим", "Мельник", 2, "ST-123460", new DateTime(2006, 5, 2));
            people[count++] = new Painter("Марія", "Бондар", "пейзаж");
            people[count++] = new Farmer("Петро", "Ткаченко", "Сонячне");
            files.Save(fileName, people, count);
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Показати всіх людей");
            Console.WriteLine("2. Додати студента");
            Console.WriteLine("3. Додати художника");
            Console.WriteLine("4. Додати фермера");
            Console.WriteLine("5. Пошук за прізвищем");
            Console.WriteLine("6. Пошук студента за квитком");
            Console.WriteLine("7. Видалити студента за квитком");
            Console.WriteLine("8. Студенти 2-го курсу, народжені взимку");
            Console.WriteLine("9. Показати вміння людей");
            Console.WriteLine("10. Зберегти у файл");
            Console.WriteLine("11. Зчитати з файлу");
            Console.WriteLine("0. Вихід");
            Console.Write("\nВаш вибір: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    ShowPeople(people, count);
                    Pause();
                    break;
                case "2":
                    Console.Clear();
                    AddStudent(people, ref count);
                    files.Save(fileName, people, count);
                    Pause();
                    break;
                case "3":
                    Console.Clear();
                    AddPainter(people, ref count);
                    files.Save(fileName, people, count);
                    Pause();
                    break;
                case "4":
                    Console.Clear();
                    AddFarmer(people, ref count);
                    files.Save(fileName, people, count);
                    Pause();
                    break;
                case "5":
                    Console.Clear();
                    Console.Write("Введіть прізвище: ");
                    string lastName = Console.ReadLine() ?? "";
                    int index = files.FindByLastName(people, count, lastName);
                    ShowSearch("Результат пошуку", people, index);
                    Pause();
                    break;
                case "6":
                    Console.Clear();
                    Console.Write("Введіть студентський квиток: ");
                    string ticket = Console.ReadLine() ?? "";
                    int ticketIndex = files.FindByTicket(people, count, ticket);
                    ShowSearch("Результат пошуку", people, ticketIndex);
                    Pause();
                    break;
                case "7":
                    Console.Clear();
                    Console.Write("Введіть студентський квиток для видалення: ");
                    string deleteTicket = Console.ReadLine() ?? "";
                    int oldCount = count;
                    count = files.DeleteByTicket(people, count, deleteTicket);
                    Console.WriteLine(count < oldCount ? "Запис видалено." : "Студента з таким квитком не знайдено.");
                    files.Save(fileName, people, count);
                    Pause();
                    break;
                case "8":
                    Console.Clear();
                    ShowWinterStudents(people, count);
                    Pause();
                    break;
                case "9":
                    Console.Clear();
                    ShowSkills(people, count);
                    Pause();
                    break;
                case "10":
                    Console.Clear();
                    files.Save(fileName, people, count);
                    Console.WriteLine("Дані збережено у файл.");
                    Pause();
                    break;
                case "11":
                    Console.Clear();
                    count = files.Load(fileName, people);
                    Console.WriteLine($"Дані зчитано з файлу. Записів: {count}");
                    Pause();
                    break;
                case "0":
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Невірний пункт меню.");
                    Pause();
                    break;
            }
        }
    }




    private static void ShowPeople(Person[] people, int count)
    {
        Console.WriteLine("=== Усі люди ===\n");

        if (count == 0)
        {
            Console.WriteLine("База даних порожня.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Person person = people[i];
            Console.WriteLine($"{i + 1}. {person.GetType().Name}: {person.FirstName} {person.LastName}");

            if (person is Student student)
            {
                Console.WriteLine($"   Курс: {student.Course}");
                Console.WriteLine($"   Квиток: {student.StudentTicket}");
                Console.WriteLine($"   Дата народження: {student.BirthDate:dd.MM.yyyy}");
            }
            else if (person is Painter painter)
            {
                Console.WriteLine($"   Стиль: {painter.Style}");
            }
            else if (person is Farmer farmer)
            {
                Console.WriteLine($"   Господарство: {farmer.Farm}");
            }

            Console.WriteLine();
        }
    }

    private static void ShowWinterStudents(Person[] people, int count)
    {
        int result = 0;
        Console.WriteLine("=== Студенти 2-го курсу, народжені взимку ===\n");

        for (int i = 0; i < count; i++)
        {
            // Зима охоплює грудень, січень і лютий, тому перевіряємо саме ці три місяці.
            if (people[i] is Student student &&
                student.Course == 2 &&
                (student.BirthDate.Month == 12 || student.BirthDate.Month <= 2))
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} — {student.BirthDate:dd.MM.yyyy}");
                result++;
            }
        }

        Console.WriteLine($"\nКількість: {result}");
    }

    private static void ShowSearch(string title, Person[] people, int index)
    {
        Console.WriteLine($"=== {title} ===\n");

        if (index == -1)
        {
            Console.WriteLine("Не знайдено.");
            return;
        }

        Person person = people[index];
        Console.WriteLine($"Тип: {person.GetType().Name}");
        Console.WriteLine($"Ім'я: {person.FirstName}");
        Console.WriteLine($"Прізвище: {person.LastName}");

        if (person is Student student)
        {
            Console.WriteLine($"Курс: {student.Course}");
            Console.WriteLine($"Квиток: {student.StudentTicket}");
            Console.WriteLine($"Дата народження: {student.BirthDate:dd.MM.yyyy}");
        }
    }

    private static void ShowSkills(Person[] people, int count)
    {
        Console.WriteLine("=== Вміння ===\n");

        for (int i = 0; i < count; i++)
        {
            Person person = people[i];
            Console.WriteLine(person.Swim());

            if (person is Student student)
                Console.WriteLine(student.Study());
            else if (person is Painter painter)
                Console.WriteLine(painter.Paint());
            else if (person is Farmer farmer)
                Console.WriteLine(farmer.Work());

            Console.WriteLine();
        }
    }

    private static void AddStudent(Person[] people, ref int count)
    {
        if (count >= people.Length)
        {
            Console.WriteLine("База заповнена.");
            return;
        }

        Console.WriteLine("=== Додавання студента ===");
        string firstName = ReadName("Ім'я: ");
        string lastName = ReadName("Прізвище: ");
        int course = ReadCourse();
        string ticket = ReadTicket();
        DateTime birthDate = ReadDate();

        people[count++] = new Student(firstName, lastName, course, ticket, birthDate);
        Console.WriteLine("Студента додано.");
    }

    private static void AddPainter(Person[] people, ref int count)
    {
        if (count >= people.Length)
        {
            Console.WriteLine("База заповнена.");
            return;
        }

        Console.WriteLine("=== Додавання художника ===");
        string firstName = ReadName("Ім'я: ");
        string lastName = ReadName("Прізвище: ");
        Console.Write("Стиль: ");
        string style = Console.ReadLine() ?? "";

        people[count++] = new Painter(firstName, lastName, style);
        Console.WriteLine("Художника додано.");
    }

    private static void AddFarmer(Person[] people, ref int count)
    {
        if (count >= people.Length)
        {
            Console.WriteLine("База заповнена.");
            return;
        }

        Console.WriteLine("=== Додавання фермера ===");
        string firstName = ReadName("Ім'я: ");
        string lastName = ReadName("Прізвище: ");
        Console.Write("Господарство: ");
        string farm = Console.ReadLine() ?? "";

        people[count++] = new Farmer(firstName, lastName, farm);
        Console.WriteLine("Фермера додано.");
    }

    private static string ReadName(string message)
    {
        while (true)
        {
            Console.Write(message);
            string value = Console.ReadLine() ?? "";

            if (Validator.IsName(value))
                return value;

            Console.WriteLine("Помилка: використовуйте тільки літери.");
        }
    }

    private static int ReadCourse()
    {
        while (true)
        {
            Console.Write("Курс (1-6): ");

            if (int.TryParse(Console.ReadLine(), out int course) && course >= 1 && course <= 6)
                return course;

            Console.WriteLine("Помилка: введіть число від 1 до 6.");
        }
    }

    private static string ReadTicket()
    {
        while (true)
        {
            Console.Write("Студентський квиток (ST-123456): ");
            string value = Console.ReadLine() ?? "";

            if (Validator.IsTicket(value))
                return value;

            Console.WriteLine("Помилка: формат повинен бути ST-123456.");
        }
    }

    private static DateTime ReadDate()
    {
        while (true)
        {
            Console.Write("Дата народження (dd.MM.yyyy): ");

            if (DateTime.TryParseExact(
                Console.ReadLine(),
                "dd.MM.yyyy",
                null,
                System.Globalization.DateTimeStyles.None,
                out DateTime date))
            {
                return date;
            }

            Console.WriteLine("Помилка: неправильна дата.");
        }
    }

    private static void Pause()
    {
        Console.WriteLine("\nНатисніть Enter, щоб повернутися до меню...");
        Console.ReadLine();
    }
}
