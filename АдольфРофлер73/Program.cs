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
        static void ViewStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Нет студентов в системе.");
                return;
            }

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        static void EnrollStudent()
        {
            ViewStudents();
            Console.Write("Введите ID студента для записи на курс: ");
            int studentId = int.Parse(Console.ReadLine());

            var student = students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                Console.WriteLine("Студент не найден.");
                return;
            }

            ViewCourses();
            Console.Write("Введите ID курса для записи: ");
            int courseId = int.Parse(Console.ReadLine());

            var course = courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                Console.WriteLine("Курс не найден.");
                return;
            }

            student.EnrollInCourse(course);
            Console.WriteLine($"Студент {student.Name} успешно записан на курс {course.Title}!");
        }
        static void AddTeacher()
        {
            Console.Write("Введите имя преподавателя: ");
            string name = Console.ReadLine();
            Console.Write("Введите возраст преподавателя: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Введите контактную информацию: ");
            string contactInfo = Console.ReadLine();

            teachers.Add(new Teacher(name, age, contactInfo));
            Console.WriteLine("Преподаватель успешно добавлен!");
        }

        static void ViewTeachers()
        {
            if (teachers.Count == 0)
            {
                Console.WriteLine("Нет преподавателей в системе.");
                return;
            }

            foreach (var teacher in teachers)
            {
                Console.WriteLine(teacher);
            }
        }

       