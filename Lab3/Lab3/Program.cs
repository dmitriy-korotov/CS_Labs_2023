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
            return new Student(RandomPerson(), RandomEducation(), random.Next(101, 599));
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

            Student student = RandomStudent();
            Student? student_cpy = null;
            try
            {
                student_cpy = student.SerializeDeepCopy();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            if (student_cpy == null)
            {
                Console.WriteLine("Student copy is null");
                return;
            }
            Console.WriteLine($"Student:\n\n{student}");
            Console.WriteLine($"\n\nStudent copy:\n\n{student_cpy}");



            writeWithColor("\n\t\tTASK 2\n");

            Console.WriteLine("Input filename:\t");
            string filepath = Console.ReadLine() ?? "Models.txt";
            if (File.Exists(filepath))
            {
                student.Load(filepath);
            }
            else
            {
                File.Create(filepath);
                Console.WriteLine($"File '{filepath}' is not exists, he was created.");
            }



            writeWithColor("\n\t\tTASK 3\n");
            Console.WriteLine(student);



            writeWithColor("\n\t\tTASK 4\n");
            student.AddFromConsole();
            student.Save(filepath);
            Console.WriteLine(student);



            writeWithColor("\n\t\tTASK 5\n");
            Student.Load(filepath, out student);
            if (student == null)
            {
                Console.WriteLine("Student after loading from file is null");
                return;
            }
            student.AddFromConsole();
            Student.Save(filepath, student);



            writeWithColor("\n\t\tTASK 6\n");
            Console.WriteLine(student);
        }
    }
}