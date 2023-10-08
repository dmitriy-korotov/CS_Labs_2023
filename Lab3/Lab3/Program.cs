using System;
using System.Collections.Generic;
using Models;

namespace Lab3
{
    class Programm
    {
        public static void Main(String[] _args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\tTASK 1\n");
            Console.ForegroundColor = ConsoleColor.White;
            Person person_1 = new Person("Name", "Surname", new DateTime(2004, 1, 1));
            Person person_2 = new Person("Name", "Surname", new DateTime(2004, 1, 1));
            Console.WriteLine($"Objects ==: {person_1 == person_2}");
            Console.WriteLine($"Objects equals: {person_1.Equals(person_2)}");
            Console.WriteLine($"First person hash: {person_1.GetHashCode()}");
            Console.WriteLine($"Second person hash: {person_2.GetHashCode()}");



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\tTASK 2\n");
            Console.ForegroundColor = ConsoleColor.White;
            Student student = new Student(person_1, Education.Bachelor, 22);
            student.addExams(new Exam("Geometry", 1, new DateTime(2004, 2, 1)), new Exam("Russian", 3, new DateTime(2006, 4, 30)), new Exam());
            student.addTests(new Test("Geometry", true), new Test("Russian", false), new Test("History", false), new Test());
            Console.WriteLine(student);



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\tTASK 3\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(student.Person);



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\tTASK 4\n");
            Console.ForegroundColor = ConsoleColor.White;
            Student student_copy = (Student)student.DeepCopy();
            student.Name = "Dmitriy";
            student.Surame = "Korotov";
            Console.WriteLine($"Student:\n{student}");
            Console.WriteLine($"Student copy:\n{student_copy}");



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\tTASK 5\n");
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                student.NumberOfGroup = 12000;
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex.Message);
            }



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\tTASK 6\n");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (object elem in student.ExamsAndTests())
            {
                Console.WriteLine(elem.ToString());
            }



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\tTASK 7\n");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (object elem in student.ExamsWithMarkMoreThen(3))
            {
                Console.WriteLine(elem.ToString());
            }



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\tTASK 8\n");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var elem in student)
            {
                Console.WriteLine(elem);
            }



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\tTASK 9\n");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var elem in student.GetPassedTestsAndExams())
            {
                Console.WriteLine(elem);
            }



            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\tTASK 10\n");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var elem in student.GetPassedTests())
            {
                Console.WriteLine(elem);
            }
        }
    }
}