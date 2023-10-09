using System;
using System.Collections.Generic;
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
            
            Student student = new Student();
            student.addExams(new Exam("Math", 4, new DateTime(2004, 2, 4)), new Exam("History", 3, new DateTime(2005, 2, 5)),
                             new Exam("Russian", 5, new DateTime(1999, 4, 4)), new Exam("Physic", 2, new DateTime(2010, 6, 4)),
                             new Exam("Subject", 1, new DateTime(2023, 2, 10)), new Exam("Subject2", 5, new DateTime(2014, 12, 14)));

            writeWithColor($"Student:\n\n");
            Console.WriteLine(student);
            student.sortExamsBySubjectName();
            writeWithColor($"Student after sorting by subject name:\n\n");
            Console.WriteLine(student);
            student.sortExamsByMark();
            writeWithColor($"Student after sorting by exams mark:\n\n");
            Console.WriteLine(student);
            student.sortExamsByDate();
            writeWithColor($"Student after sorting by date:\n\n");
            Console.WriteLine(student);



            writeWithColor("\n\t\tTASK 2\n");
            StudentCollection<string> studen_collection = new StudentCollection<string>(student => RandomString(student.Surame.Length));
            studen_collection.AddStudents(RandomStudents(5));
            Console.WriteLine(studen_collection);



            writeWithColor("\n\t\tTASK 3\n");
            writeWithColor($"MaxAverageMark = {studen_collection.MaxAverageMark}\n");
            writeWithColor($"EducationForm:\n");
            foreach (var stud in studen_collection.EducationForm(Education.Specialist))
            {
                Console.WriteLine(stud.Value);
            }
            writeWithColor($"GroupByEducation:\n");
            foreach (var group in studen_collection.GroupByEducation)
            {
                foreach (var stud in group)
                {
                    Console.WriteLine(stud.Value);
                }
            }



            writeWithColor("\n\t\tTASK 4\n");
            GenerateElement<Person, Student> generator = index => new KeyValuePair<Person, Student>(RandomPerson(), RandomStudent());
            TestCollections<Person, Student> test_collections = new TestCollections<Person, Student>(10, generator);
        }
    }
}