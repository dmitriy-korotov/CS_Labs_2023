using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using Lab3.models;
using Models;

namespace Lab3
{
    class Programm
    {
        private static Random random = new Random();



        private static string RandomString(int _length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, _length).Select(s => s[random.Next(s.Length)]).ToArray());
        }



        private static DateTime RandomDate()
        {
            return new DateTime(random.Next(2023), random.Next(1, 12), random.Next(1, 28));
        }


        private static Education RandomEducation()
        {
            Education[] education_types = { Education.Specialist, Education.Bachelor, Education.SecondEducation };
            return education_types[random.Next(0, 3)];
        }



        private static Exam[] RandomExams(int _count)
        {
            List<Exam> exams = new List<Exam>();
            for (int i = 0; i < _count; i++)
            {
                Exam exam = new Exam(RandomString(random.Next(5, 10)), random.Next(1, 5), RandomDate());
                exams.Add(exam);
            }
            return exams.ToArray();
        }



        private static Person RandomPerson()
        {
            return new Person(RandomString(10), RandomString(7), RandomDate());
        }



        private static Student RandomStudent()
        {
            return new Student(RandomPerson(), RandomEducation(), random.Next(100));
        }



        private static Student[] RandomStudents(int _count)
        {
            List<Student> students = new List<Student>();
            for (int i = 0; i < _count; i++)
            {
                Student student = RandomStudent();
                students.Add(student);
                student.addExams(RandomExams(random.Next(3, 5)));
            }
            return students.ToArray();
        }



        private static void writeWithColor(string _message, ConsoleColor _color = ConsoleColor.Green)
        {
            Console.ForegroundColor = _color;
            Console.WriteLine(_message);
            Console.ForegroundColor = ConsoleColor.White;
        }



        public static void Main(String[] _args)
        {
            writeWithColor("\n\t\tTASK 1\n");

            Dictionary<Student, String> students = new Dictionary<Student, String>();

            StudentCollection<string> student_cllection_1 = new StudentCollection<string>("Student collection 1", student =>
            {
                if (students.ContainsKey(student))
                    return students.GetValueOrDefault(student, "key");

                string key = RandomString(10);
                students.Add(student, key);
                return key;
            });
            Console.WriteLine(student_cllection_1);

            StudentCollection<string> student_cllection_2 = new StudentCollection<string>("Student collection 2", student =>
            {
                if (students.ContainsKey(student))
                    return students.GetValueOrDefault(student, "key");

                string key = RandomString(10);
                students.Add(student, key);
                return key;
            });
            Console.WriteLine(student_cllection_2);



            writeWithColor("\n\t\tTASK 2\n");
            Journal journal = new Journal();
            student_cllection_1.StudentsChanged += journal.StudentChangedHandler;
            student_cllection_2.StudentsChanged += journal.StudentChangedHandler;



            writeWithColor("\n\t\tTASK 3\n");

            for (int i = 0; i < Random.Shared.NextInt64(5, 10); i++)
            {
                student_cllection_1.AddStudents(RandomStudents(3));
            }
            for (int i = 0; i < Random.Shared.NextInt64(5, 10); i++)
            {
                student_cllection_2.AddStudents(RandomStudents(3));
            }

            for (int i = 0; i < Random.Shared.Next(5, 15); i++)
            {
                foreach (var student in student_cllection_1.EducationForm(Education.Bachelor))
                {
                    student.Value.NumberOfGroup = Random.Shared.Next(101, 599);
                    student.Value.EducationType = RandomEducation();
                }
            }

            for (int i = 0; i < Random.Shared.Next(1, students.Count); i++)
            {
                var pair = students.ElementAt(Random.Shared.Next(0, students.Count - 1));

                student_cllection_1.Remove(pair.Key);
                student_cllection_2.Remove(pair.Key);

                pair.Key.EducationType = RandomEducation();
            }



            writeWithColor("\n\t\tTASK 4\n");

            writeWithColor($"Journal:\n\n");
            Console.WriteLine(journal);
        }
    }
}