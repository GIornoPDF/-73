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

        static void CreateCourse()
        {
            ViewTeachers();
            Console.Write("Введите ID преподавателя для курса: ");
            int teacherId = int.Parse(Console.ReadLine());

            var teacher = teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher == null)
            {
                Console.WriteLine("Преподаватель не найден.");
                return;
            }

            Console.Write("Введите название курса: ");
            string title = Console.ReadLine();

            courses.Add(new Course(title, teacher));
            Console.WriteLine($"Курс '{title}' успешно создан!");
        }

        static void ViewCourses()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("Нет курсов в системе.");
                return;
            }

            foreach (var course in courses)
            {
                Console.WriteLine(course);
                if (course.EnrolledStudents.Count > 0)
                {
                    Console.WriteLine("Записанные студенты:");
                    foreach (var student in course.EnrolledStudents)
                    {
                        Console.WriteLine($" - {student.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("Нет записанных студентов.");
                }
            }
        }
    }

    abstract class Person
    {
        private static int _idCounter = 1;

        public int Id { get; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string ContactInfo { get; private set; }

        protected Person(string name, int age, string contactInfo)
        {
            Id = _idCounter++;
            Name = name;
            Age = age;
            ContactInfo = contactInfo;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Имя: {Name}, Возраст: {Age}, Контактная информация: {ContactInfo}";
        }
    }

    class Student : Person
    {
        public List<Course> EnrolledCourses { get; private set; } = new List<Course>();

        public Student(string name, int age, string contactInfo) : base(name, age, contactInfo) { }

        public void EnrollInCourse(Course course)
        {
            EnrolledCourses.Add(course);
            course.AddStudent(this);
        }

        public override string ToString()
        {
            return base.ToString() + $", Записанные курсы: {EnrolledCourses.Count}";
        }
    }

    class Teacher : Person
    {
        public Teacher(string name, int age, string contactInfo) : base(name, age, contactInfo) { }
    }

    class Course
    {
        private static int _idCounter = 1;

        public int Id { get; }
        public string Title { get; private set; }
        public Teacher Instructor { get; private set; }
        public List<Student> EnrolledStudents { get; private set; } = new List<Student>();

        public Course(string title, Teacher instructor)
        {
            Id = _idCounter++;
            Title = title;
            Instructor = instructor;
        }
        public void AddStudent(Student student)
        {
            EnrolledStudents.Add(student);
        }

        public override string ToString()
        {
            return $"ID: {Id}, Название курса: {Title}, Преподаватель: {Instructor.Name}";
        }
    }
}


