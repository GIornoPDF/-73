using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        List<TextStatistics> statisticsList = new List<TextStatistics>();
        bool continueInput = true;

        while (continueInput)
        {
            Console.WriteLine("Введите текст (минимум 100 символов):");
            string inputText = Console.ReadLine();

            if (inputText.Length < 100)
            {
                Console.WriteLine("Текст должен содержать минимум 100 символов. Попробуйте снова.");
                continue;
            }

            var stats = AnalyzeText(inputText);
            statisticsList.Add(stats);

            Console.WriteLine("\nСтатистика по введённому тексту:");
            DisplayStatistics(stats);

            Console.WriteLine("\nХотите ввести новый текст? (да/нет)");
            string response = Console.ReadLine().ToLower();
            continueInput = response == "да";
        }

        Console.WriteLine("\nСтатистика по всем текстам:");
        foreach (var stat in statisticsList)
        {
            DisplayStatistics(stat);
        }
    }
    static TextStatistics AnalyzeText(string text)
    {
        var words = Regex.Split(text, @"W+").Where(w => !string.IsNullOrEmpty(w)).ToList();
        var sentences = Regex.Split(text, @"[.!?]").Where(s => !string.IsNullOrEmpty(s)).ToList();
        int vowelsCount = text.Count(c => "аеёиоуыэюяАЕЁИОУЫЭЮЯaeiouyAEIOUY".Contains(c));
        int consonantsCount = text.Count(c => "бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ".Contains(c));

        string shortestWord = words.OrderBy(w => w.Length).FirstOrDefault();
        string longestWord = words.OrderByDescending(w => w.Length).FirstOrDefault();
        var letterFrequency = new Dictionary<char, int>();
        foreach (var c in text.ToLower())
        {
            if (char.IsLetter(c))
            {
                if (letterFrequency.ContainsKey(c))
                    letterFrequency[c]++;
                else
                    letterFrequency[c] = 1;
            }
        }

        return new TextStatistics
        {
            WordCount = words.Count,
            SentenceCount = sentences.Count,
            ShortestWord = shortestWord,
            LongestWord = longestWord,
            VowelsCount = vowelsCount,
            ConsonantsCount = consonantsCount,
            LetterFrequency = letterFrequency
        };
    }

    static void DisplayStatistics(TextStatistics stats)
    {
        Console.WriteLine($"Количество слов: {stats.WordCount}");
        Console.WriteLine($"Количество предложений: {stats.SentenceCount}");
        Console.WriteLine($"Самое короткое слово: {stats.ShortestWord}");
        Console.WriteLine($"Самое длинное слово: {stats.LongestWord}");
        Console.WriteLine($"Количество гласных букв: {stats.VowelsCount}");
        Console.WriteLine($"Количество согласных букв: {stats.ConsonantsCount}");

        Console.WriteLine("Частота встречаемости каждой буквы:");
        foreach (var kvp in stats.LetterFrequency.OrderBy(kvp => kvp.Key))
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}

class TextStatistics
{
    public int WordCount { get; set; }
    public int SentenceCount { get; set; }
    public string ShortestWord { get; set; }
    public string LongestWord { get; set; }
    public int VowelsCount { get; set; }
    public int ConsonantsCount { get; set; }
    public Dictionary<char, int> LetterFrequency { get; set; }
}
