using System;
using System.Collections.Generic;
using System.Linq;
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


            int count = 0;

            Console.WriteLine("Input elements count:\t");
            string line = Console.ReadLine();
            if (line != null)
            {
                Int32.TryParse(line, out count);
            }

            while (count < 1)
            {
                Console.WriteLine("Input correct value elements count:\t");
                line = Console.ReadLine();
                if (line != null)
                {
                    Int32.TryParse(line, out count);
                }
            }

            Dictionary<int, KeyValuePair<Person, Student>> dict1 = new();
            Dictionary<int, Person> dict2 = new();
            Dictionary<int, KeyValuePair<string, Student>> dict3 = new();
            Dictionary<int, string> dict4 = new();

            GenerateElement<Person, Student> generator = index => {
                    var pair = new KeyValuePair<Person, Student>(RandomPerson(), RandomStudent());
                    dict1.Add(index, pair);
                    dict2.Add(index, pair.Key);
                    return pair;
                };
            TestCollections<Person, Student> test_collections = new TestCollections<Person, Student>(count, generator);

            test_collections.fillStringsList(count, index => {
                    string str = RandomString(10);
                    dict4.Add(index, str);
                    return str;
            });
            test_collections.fillDictWithKeyString(count, index => {
                    var pair = new KeyValuePair<string, Student>(RandomString(10), RandomStudent());
                    dict3.Add(index, pair);
                    return pair;
            });
            foreach (var el in test_collections.ListKeys)
            {
                Console.WriteLine(el);
            }
            Console.WriteLine("\n\n");
            foreach (var el in test_collections.Dict1)
            {
                Console.WriteLine(el.Key);
                Console.WriteLine(el.Value);
            }
            Console.WriteLine("\n\n");
            foreach (var el in test_collections.Dict2)
            {
                Console.WriteLine(el.Key);
                Console.WriteLine(el.Value);
            }
            Console.WriteLine("\n\n");
            foreach (var el in test_collections.ListStrings)
            {
                Console.WriteLine(el);
            }



            writeWithColor("\n\nTKey && Tvalue\n\n");

            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

            timer.Start();
            test_collections.FindInDict1(dict1.ElementAt(0).Value.Key);
            timer.Stop();
            writeWithColor($"Find first: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInDict1(dict1.ElementAt(dict1.Count / 2).Value.Key);
            timer.Stop();
            writeWithColor($"Find middle: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInDict1(dict1.ElementAt(dict1.Count - 1).Value.Key);
            timer.Stop();
            writeWithColor($"Find last: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInDict1(RandomPerson());
            writeWithColor($"Find not existed: {timer.Elapsed}");
            timer.Stop();
            timer.Reset();



            writeWithColor("\n\nTKey\n\n");



            timer.Start();
            test_collections.FindInKeysList(dict2.ElementAt(0).Value);
            timer.Stop();
            writeWithColor($"Find first: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInKeysList(dict2.ElementAt(dict2.Count / 2).Value);
            timer.Stop();
            writeWithColor($"Find middle: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInKeysList(dict2.ElementAt(dict2.Count - 1).Value);
            timer.Stop();
            writeWithColor($"Find last: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInKeysList(RandomPerson());
            writeWithColor($"Find not existed: {timer.Elapsed}");
            timer.Stop();
            timer.Reset();



            writeWithColor("\n\nstring && Student\n\n");



            timer.Start();
            test_collections.FindInDict2(dict3.ElementAt(0).Value.Key);
            timer.Stop();
            writeWithColor($"Find first: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInDict2(dict3.ElementAt(dict3.Count / 2).Value.Key);
            timer.Stop();
            writeWithColor($"Find middle: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInDict2(dict3.ElementAt(dict3.Count - 1).Value.Key);
            timer.Stop();
            writeWithColor($"Find last: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInDict2(RandomString(15));
            writeWithColor($"Find not existed: {timer.Elapsed}");
            timer.Stop();
            timer.Reset();



            writeWithColor("\n\nstring\n\n");



            timer.Start();
            test_collections.FindInStringsList(dict4.ElementAt(0).Value);
            timer.Stop();
            writeWithColor($"Find first: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInStringsList(dict4.ElementAt(dict4.Count / 2).Value);
            timer.Stop();
            writeWithColor($"Find middle: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInStringsList(dict4.ElementAt(dict4.Count - 1).Value);
            timer.Stop();
            writeWithColor($"Find last: {timer.Elapsed}");
            timer.Reset();

            timer.Start();
            test_collections.FindInStringsList(RandomString(15));
            writeWithColor($"Find not existed: {timer.Elapsed}");
            timer.Stop();
            timer.Reset();
        }
    }
}