using System;
using System.Text;
using System.Text.RegularExpressions;

class RegexTextAnalyzer
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Работа с регулярными выражениями (русский текст) ===\n");

        // 1. Русский текст (из вашего файла)
        string text = "Бык тупогуб, тупогубенький бычок, у быка губа бела была тупа. " +
                      "Мама мыла раму. Привет мир! Это тестовый текст. " +
                      "123-456-7890 - номер телефона. " +
                      "Москва, Санкт-Петербург, Новосибирск. " +
                      "Регулярные выражения в C# очень полезны.";

        Console.WriteLine("Исходный текст:");
        Console.WriteLine(text);
        Console.WriteLine();

        // 2. Подсчёт символов и слов
        CountCharactersAndWords(text);

        // 3. Строки, начинающиеся с определённого символа (слова)
        LinesStartingWith(text, "Бык");

        // 4. Строки, оканчивающиеся определённым символом
        LinesEndingWith(text, ".");

        // 5. Замена части текста
        ReplaceTextExample(text);
    }

    static void CountCharactersAndWords(string text)
    {
        Console.WriteLine("=== 2. Подсчёт символов и слов ===");

        // Буквы и цифры (русские + английские + цифры)
        Regex lettersRegex = new Regex(@"[A-Za-zА-Яа-я0-9]");
        int letterCount = lettersRegex.Matches(text).Count;
        Console.WriteLine($"Количество букв и цифр: {letterCount}");

        // Все символы
        Console.WriteLine($"Общее количество символов: {text.Length}");

        // Количество слов
        Regex wordRegex = new Regex(@"\b\w+\b");
        int wordCount = wordRegex.Matches(text).Count;
        Console.WriteLine($"Количество слов: {wordCount}");

        // Количество словосочетаний (слово "тупогуб")
        Regex phraseRegex = new Regex(@"тупогуб\w*");
        int phraseCount = phraseRegex.Matches(text).Count;
        Console.WriteLine($"Количество слов с корнем 'тупогуб': {phraseCount}");

        Console.WriteLine();
    }

    static void LinesStartingWith(string text, string startWord)
    {
        Console.WriteLine($"=== 3. Строки, начинающиеся с '{startWord}' ===");

        string[] lines = text.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

        Regex regex = new Regex($"^{startWord}", RegexOptions.IgnoreCase);
        int found = 0;

        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (regex.IsMatch(trimmed))
            {
                Console.WriteLine(trimmed);
                found++;
            }
        }

        if (found == 0)
            Console.WriteLine("Не найдено.");
        else
            Console.WriteLine($"Найдено строк: {found}");

        Console.WriteLine();
    }

    static void LinesEndingWith(string text, string endSymbol)
    {
        Console.WriteLine($"=== 4. Строки, оканчивающиеся на '{endSymbol}' ===");

        string[] lines = text.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

        string escaped = Regex.Escape(endSymbol);
        Regex regex = new Regex($"{escaped}$");

        int found = 0;

        foreach (string line in lines)
        {
            string trimmed = line.Trim() + ".";
            if (regex.IsMatch(trimmed))
            {
                Console.WriteLine(trimmed);
                found++;
            }
        }

        if (found == 0)
            Console.WriteLine("Не найдено.");
        else
            Console.WriteLine($"Найдено строк: {found}");

        Console.WriteLine();
    }

    static void ReplaceTextExample(string text)
    {
        Console.WriteLine("=== 5. Замена части текста ===");

        // Замена нескольких пробелов на один
        Regex spaceRegex = new Regex(@"\s+");
        string result1 = spaceRegex.Replace(text, " ");
        Console.WriteLine("После замены нескольких пробелов на один:");
        Console.WriteLine(result1);
        Console.WriteLine();

        // Замена цифр на #
        Regex digitRegex = new Regex(@"\d");
        string result2 = digitRegex.Replace(text, "#");
        Console.WriteLine("После замены цифр на #:");
        Console.WriteLine(result2);
        Console.WriteLine();

        // Замена слова "тупогуб" на "****"
        Regex wordRegex = new Regex(@"тупогуб\w*");
        string result3 = wordRegex.Replace(text, "****");
        Console.WriteLine("После замены 'тупогуб' на '****':");
        Console.WriteLine(result3);
    }
}