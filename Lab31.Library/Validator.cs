using System.Text.RegularExpressions;

namespace Lab31.Library;

public static class Validator
{
    // Перевірки винесені сюди, щоб меню не містило деталей регулярних виразів.
    public static bool IsName(string value) =>
        Regex.IsMatch(value, @"^[А-ЯІЇЄҐа-яіїєґA-Za-z'-]+$");

    public static bool IsTicket(string value) =>
        Regex.IsMatch(value, @"^ST-\d{6}$");
}
