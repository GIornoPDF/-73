using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Book> books = new List<Book>();
    static int nextId = 1;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nКоманды:");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Удалить книгу");
            Console.WriteLine("3. Найти книги");
            Console.WriteLine("4. Отсортировать книги");
            Console.WriteLine("5. Вывести самую дорогую и самую дешёвую книгу");
            Console.WriteLine("6. Сгруппировать книги по авторам");
            Console.WriteLine("0. Выход");
            Console.Write("Введите номер команды: ");
            string command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    RemoveBook();
                    break;
                case "3":
                    FindBooks();
                    break;
                case "4":
                    SortBooks();
                    break;
                case "5":
                    ShowPriceRange();
                    break;
                case "6":
                    GroupBooksByAuthor();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неизвестная команда. Попробуйте снова.");
                    break;
            }
        }
    }

    static void AddBook()
    {
        Console.Write("Введите название книги: ");
        string title = Console.ReadLine();

        Console.Write("Введите автора книги: ");
        string author = Console.ReadLine();

        Console.WriteLine("Выберите жанр (1 - Фантастика, 2 - Роман, 3 - Приключения): ");
        string genreInput = Console.ReadLine();
        string genre = genreInput switch
        {
            "1" => "Фантастика",
            "2" => "Роман",
            "3" => "Приключения",
            _ => "Неизвестный"
        };

        Console.Write("Введите год издания: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Введите цену книги: ");
        decimal price = decimal.Parse(Console.ReadLine());

        books.Add(new Book
        {
            Id = nextId++,
            Title = title,
            Author = author,
            Genre = genre,
            Year = year,
            Price = price
        });

        Console.WriteLine("Книга успешно добавлена!");
    }

    static void RemoveBook()
    {
        Console.Write("Введите идентификатор книги для удаления: ");
        int id = int.Parse(Console.ReadLine());

        var bookToRemove = books.FirstOrDefault(b => b.Id == id);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine("Книга успешно удалена!");
        }
        else
        {
            Console.WriteLine("Книга с таким идентификатором не найдена.");
        }
    }

    static void FindBooks()
    {
        Console.WriteLine("Поиск книг по:");
        Console.WriteLine("1. Названию");
        Console.WriteLine("2. Автору");
        Console.WriteLine("3. Жанру");
        string searchChoice = Console.ReadLine();

        IEnumerable<Book> foundBooks = Enumerable.Empty<Book>();

        switch (searchChoice)
        {
            case "1":
                Console.Write("Введите название книги: ");
                string titleSearch = Console.ReadLine();
                foundBooks = books.Where(b => b.Title.Contains(titleSearch, StringComparison.OrdinalIgnoreCase));
                break;
            case "2":
                Console.Write("Введите автора книги: ");
                string authorSearch = Console.ReadLine();
                foundBooks = books.Where(b => b.Author.Contains(authorSearch, StringComparison.OrdinalIgnoreCase));
                break;
            case "3":
                Console.Write("Введите жанр книги: ");
                string genreSearch = Console.ReadLine();
                foundBooks = books.Where(b => b.Genre.Equals(genreSearch, StringComparison.OrdinalIgnoreCase));
                break;
            default:
                Console.WriteLine("Неизвестный выбор.");
                return;
        }

        if (!foundBooks.Any())
        {
            Console.WriteLine("Книги не найдены.");
            return;
        }

        foreach (var book in foundBooks)
        {
            Console.WriteLine(book);
        }
    }

   