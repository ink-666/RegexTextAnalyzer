using System; // Подключает базовые возможности C#: Console, StringSplitOptions и другие
using System.Text; // Нужен для Encoding.UTF8, чтобы русские буквы нормально отображались в консоли
using System.Text.RegularExpressions; // Подключает регулярные выражения: Regex, Matches, Replace

// Класс программы
class RegexTextAnalyzer
{
    // Главный метод программы. С него начинается выполнение
    static void Main()
    {
        // Устанавливаем кодировку UTF-8 для корректного вывода русского текста в консоль
        Console.OutputEncoding = Encoding.UTF8;

        // Выводим заголовок программы
        Console.WriteLine("=== Работа с регулярными выражениями (русский текст) ===\n");

        // Создаём строку с русским текстом, который будем анализировать
        string text = "Бык тупогуб, тупогубенький бычок, у быка губа бела была тупа. " +
                      "Мама мыла раму. Привет мир! Это тестовый текст. " +
                      "123-456-7890 - номер телефона. " +
                      "Москва, Санкт-Петербург, Новосибирск. " +
                      "Регулярные выражения в C# очень полезны.";

        // Выводим подпись перед исходным текстом
        Console.WriteLine("Исходный текст:");

        // Выводим сам текст
        Console.WriteLine(text);

        // Пустая строка для красивого разделения вывода
        Console.WriteLine();

        // Вызываем метод, который считает символы, слова и слова с нужным корнем
        CountCharactersAndWords(text);

        // Вызываем метод, который ищет предложения, начинающиеся со слова "Бык"
        LinesStartingWith(text, "Бык");

        // Вызываем метод, который ищет предложения, оканчивающиеся на точку
        LinesEndingWith(text, ".");

        // Вызываем метод, который показывает примеры замены текста через Regex
        ReplaceTextExample(text);
    }

    // Метод для подсчёта букв, цифр, всех символов, слов и слов с определённым корнем
    static void CountCharactersAndWords(string text)
    {
        // Выводим заголовок раздела
        Console.WriteLine("=== 2. Подсчёт символов и слов ===");

        // Создаём регулярное выражение, которое ищет английские буквы, русские буквы и цифры
        Regex lettersRegex = new Regex(@"[A-Za-zА-Яа-я0-9]");

        // Считаем количество найденных букв и цифр в тексте
        int letterCount = lettersRegex.Matches(text).Count;

        // Выводим количество букв и цифр
        Console.WriteLine($"Количество букв и цифр: {letterCount}");

        // text.Length считает вообще все символы: буквы, пробелы, знаки препинания и цифры
        Console.WriteLine($"Общее количество символов: {text.Length}");

        // Регулярное выражение для поиска слов
        // \b означает границу слова, \w+ означает одну или больше букв/цифр/символов слова
        Regex wordRegex = new Regex(@"\b\w+\b");

        // Считаем количество слов в тексте
        int wordCount = wordRegex.Matches(text).Count;

        // Выводим количество слов
        Console.WriteLine($"Количество слов: {wordCount}");

        // Регулярное выражение ищет слова, начинающиеся с "тупогуб"
        // \w* означает, что после "тупогуб" может быть любое продолжение слова
        Regex phraseRegex = new Regex(@"тупогуб\w*");

        // Считаем количество таких слов
        int phraseCount = phraseRegex.Matches(text).Count;

        // Выводим результат
        Console.WriteLine($"Количество слов с корнем 'тупогуб': {phraseCount}");

        // Пустая строка для разделения блоков вывода
        Console.WriteLine();
    }

    // Метод ищет предложения, которые начинаются с указанного слова
    static void LinesStartingWith(string text, string startWord)
    {
        // Выводим заголовок с тем словом, которое ищем в начале предложения
        Console.WriteLine($"=== 3. Строки, начинающиеся с '{startWord}' ===");

        // Делим текст на части по точкам
        // RemoveEmptyEntries убирает пустые элементы, если они появятся после разделения
        string[] lines = text.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

        // Создаём Regex, который проверяет начало строки
        // ^ означает начало строки
        // IgnoreCase позволяет не учитывать регистр букв
        Regex regex = new Regex($"^{startWord}", RegexOptions.IgnoreCase);

        // Переменная для подсчёта найденных строк
        int found = 0;

        // Перебираем каждую часть текста
        foreach (string line in lines)
        {
            // Убираем лишние пробелы в начале и конце строки
            string trimmed = line.Trim();

            // Проверяем, начинается ли строка с нужного слова
            if (regex.IsMatch(trimmed))
            {
                // Если строка подходит, выводим её
                Console.WriteLine(trimmed);

                // Увеличиваем счётчик найденных строк
                found++;
            }
        }

        // Если ничего не нашли, выводим сообщение
        if (found == 0)
            Console.WriteLine("Не найдено.");
        else
            // Иначе выводим количество найденных строк
            Console.WriteLine($"Найдено строк: {found}");

        // Пустая строка для разделения вывода
        Console.WriteLine();
    }

    // Метод ищет предложения, которые заканчиваются указанным символом
    static void LinesEndingWith(string text, string endSymbol)
    {
        // Выводим заголовок раздела
        Console.WriteLine($"=== 4. Строки, оканчивающиеся на '{endSymbol}' ===");

        // Разделяем текст на части по точкам
        string[] lines = text.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

        // Экранируем символ, чтобы Regex воспринимал его как обычный символ
        // Например, точка в Regex означает "любой символ", поэтому её нужно экранировать
        string escaped = Regex.Escape(endSymbol);

        // Создаём Regex для проверки конца строки
        // $ означает конец строки
        Regex regex = new Regex($"{escaped}$");

        // Счётчик найденных строк
        int found = 0;

        // Перебираем каждую часть текста
        foreach (string line in lines)
        {
            // Убираем пробелы и добавляем точку обратно, потому что Split удалил её
            string trimmed = line.Trim() + ".";

            // Проверяем, заканчивается ли строка нужным символом
            if (regex.IsMatch(trimmed))
            {
                // Выводим найденную строку
                Console.WriteLine(trimmed);

                // Увеличиваем счётчик
                found++;
            }
        }

        // Если ничего не найдено
        if (found == 0)
            Console.WriteLine("Не найдено.");
        else
            // Если найдено, выводим количество
            Console.WriteLine($"Найдено строк: {found}");

        // Пустая строка
        Console.WriteLine();
    }

    // Метод показывает примеры замены текста с помощью регулярных выражений
    static void ReplaceTextExample(string text)
    {
        // Выводим заголовок раздела
        Console.WriteLine("=== 5. Замена части текста ===");

        // Регулярное выражение для поиска одного или нескольких пробельных символов
        // \s означает пробел, табуляцию или перенос строки
        // + означает "один или больше"
        Regex spaceRegex = new Regex(@"\s+");

        // Заменяем все последовательности пробелов на один пробел
        string result1 = spaceRegex.Replace(text, " ");

        // Выводим результат первой замены
        Console.WriteLine("После замены нескольких пробелов на один:");
        Console.WriteLine(result1);
        Console.WriteLine();

        // Регулярное выражение для поиска цифр
        // \d означает любую цифру от 0 до 9
        Regex digitRegex = new Regex(@"\d");

        // Заменяем каждую цифру на символ #
        string result2 = digitRegex.Replace(text, "#");

        // Выводим результат второй замены
        Console.WriteLine("После замены цифр на #:");
        Console.WriteLine(result2);
        Console.WriteLine();

        // Регулярное выражение ищет слова, начинающиеся с "тупогуб"
        Regex wordRegex = new Regex(@"тупогуб\w*");

        // Заменяем найденные слова на ****
        string result3 = wordRegex.Replace(text, "****");

        // Выводим результат третьей замены
        Console.WriteLine("После замены 'тупогуб' на '****':");
        Console.WriteLine(result3);
    }
}
