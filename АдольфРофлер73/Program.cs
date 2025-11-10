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

   