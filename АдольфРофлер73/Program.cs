using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversityManagementSystem
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static List<Teacher> teachers = new List<Teacher>();
        static List<Course> courses = new List<Course>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nУправление университетом:");
                Console.WriteLine("1. Добавить студента");
                Console.WriteLine("2. Просмотреть информацию о студентах");
                Console.WriteLine("3. Записать студента на курс");
                Console.WriteLine("4. Добавить преподавателя");
                Console.WriteLine("5. Просмотреть информацию о преподавателях");
                Console.WriteLine("6. Создать курс");
                Console.WriteLine("7. Просмотреть информацию о курсах");
                Console.WriteLine("0. Выход");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ViewStudents();
                        break;
                    case "3":
                        EnrollStudent();
                        break;
                    case "4":
                        AddTeacher();
                        break;
                    case "5":
                        ViewTeachers();
                        break;
                    case "6":
                        CreateCourse();
                        break;
                    case "7":
                        ViewCourses();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void AddStudent()
        {
            Console.Write("Введите имя студента: ");
            string name = Console.ReadLine();
            Console.Write("Введите возраст студента: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Введите контактную информацию: ");
            string contactInfo = Console.ReadLine();

            students.Add(new Student(name, age, contactInfo));
            Console.WriteLine("Студент успешно добавлен!");
        }

       