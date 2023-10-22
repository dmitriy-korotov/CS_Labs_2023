using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Unicode;
using System.Xml;
using System.Xml.Serialization;

namespace Models
{
    public class Student : Person, INotifyPropertyChanged
    {
        private Education m_education_type = new();
        private int m_number_of_group = new();
        private List<Test> m_tests = new();
        private List<Exam> m_exams = new();


        public Education EducationType
        {
            get => m_education_type;
            set
            {
                m_education_type = value;
                NotifyPropertyChanged("m_education_type");
            }
        }


        public Person Person
        {
            get => (Person)base.DeepCopy();
            set
            {
                m_name = value.Name;
                m_surname = value.Surame;
                m_birthday_date = value.Date;
            }
        }


        public int NumberOfGroup
        {
            get => m_number_of_group;
            set
            {
                if (value < 101 || value > 599)
                {
                    throw new Exception("Invalid number of group value (need 100 < value < 600)");
                }
                m_number_of_group = value;
                NotifyPropertyChanged("m_number_of_group");
            }
        }


        public List<Exam> Exams
        {
            get => m_exams;
            set => m_exams = value;
        }


        public List<Test> Tests
        {
            get => m_tests;
            set => m_tests = value;
        }


        public double AverageMark
        {
            get
            {
                if (m_exams.Count == 0)
                {
                    return 0;
                }

                double sum = 0;
                foreach (Exam exam in m_exams)
                {
                    sum += exam.Mark;
                }
                return sum / m_exams.Count;
            }
        }


        public IEnumerable<object> ExamsAndTests()
        {
            foreach (Exam exam in m_exams)
            {
                yield return exam;
            }
            foreach (Test test in m_tests)
            {
                yield return test;
            }
        }


        public IEnumerable<Exam> ExamsWithMarkMoreThen(int _mark)
        {
            foreach (Exam exam in m_exams)
            {
                if (exam.Mark > _mark)
                {
                    yield return exam;
                }
            }
        }


        public bool this[Education _education_type]
        {
            get => m_education_type == _education_type;
        }


        public Student(Person _person, Education _education_type, int _number_of_group)
                : base(_person.Name, _person.Surame, _person.Date)
        {
            
            m_education_type = _education_type;
            m_number_of_group = _number_of_group;
        }


        public Student() : this(new Person(), Education.Bachelor, 22)
        { }


        public override string ToString()
        {
            string result_string = $"Student: {base.ToString()}\nEducation type: {m_education_type}\nExams list:\n";
            foreach (Exam exam in m_exams)
            {
                result_string += $"\t{exam}\n";
            }
            result_string += "\nTests list:\n";
            foreach (Test test in m_tests)
            {
                result_string += $"\t{test}\n";
            }
            return result_string;
        }


        public override string ToShortString()
        {
            string result_string = $"Student: {base.ToString()}\nEducation type: {m_education_type}\nAvarege mark: {AverageMark}\n";
            return result_string;
        }


        public void addExams(params Exam[] _exams)
        {
            for (int i = 0; i < _exams.Length; i++)
            {
                m_exams.Add(_exams[i]);
            }
        }


        public void addTests(params Test[] _tests)
        {
            for (int i = 0; i < _tests.Length; i++)
            {
                m_tests.Add(_tests[i]);
            }
        }


        public override object DeepCopy()
        {
            Student copy = new Student(new Person(m_name, m_surname, m_birthday_date), m_education_type, m_number_of_group);
            copy.addExams(m_exams.ToArray());
            copy.addTests(m_tests.ToArray());
            return copy;
        }


        public Student? SerializeDeepCopy()
        {
            using (var ms = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                return serializer.Deserialize(ms) as Student;
            }
        }


        public bool Save(string _filename)
        {
            TextWriter? writer = null;
            try
            {
                var serializer = new XmlSerializer(this.GetType());
                writer = new StreamWriter(_filename, false);
                serializer.Serialize(writer, this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

            if (writer != null)
                writer.Close();
            else
                return false;

            return true;
        }


        public static bool Save(string _filename, Student _student, bool _append = false)
        {
            TextWriter? writer = null;
            try
            {
                var serializer = new XmlSerializer(_student.GetType());
                writer = new StreamWriter(_filename, _append);
                serializer.Serialize(writer, _student);
                return true;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }


        public bool Load(string _filename)
        {
            TextReader? reader = null;
            try
            {
                var serializer = new XmlSerializer(this.GetType());
                reader = new StreamReader(_filename);
                Student? student = serializer.Deserialize(reader) as Student;
                if (student == null)
                    return false;

                this.Person = student.Person;
                this.m_education_type = student.m_education_type;
                this.m_exams = student.m_exams;
                this.m_tests = student.m_tests;
                this.m_number_of_group = student.m_number_of_group;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            if (reader != null)
                reader.Close();
            else
                return false;

            return true;
        }


        public static bool Load(string _filename, out Student? _student)
        {
            TextReader? reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(Student));
                reader = new StreamReader(_filename);
                if (serializer.Deserialize(reader) as Student == null)
                {
                    _student = null;
                    return false;
                }
                _student = serializer.Deserialize(reader) as Student;
                return true;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }



        public new bool AddFromConsole()
        {
            Person.AddFromConsole();



            Console.WriteLine("Input education type (B or S or SE)");
            while (true)
            {
                string type = Console.ReadLine() ?? "";
                if (type == "B")
                {
                    m_education_type = Education.Bachelor;
                    break;
                }
                if (type == "S")
                {
                    m_education_type = Education.Specialist;
                    break;
                }
                if (type == "SE")
                {
                    m_education_type = Education.SecondEducation;
                    break;
                }
                Console.WriteLine("Please input correct education type (B or S or SE)");
            }



            Console.WriteLine("Input number of group:\t");
            while (true)
            {
                try
                {
                    NumberOfGroup = Int32.Parse(Console.ReadLine() ?? "0");
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input correct number of group:\t");
                }
            }



            Console.WriteLine("Input exams count:\t");
            int exams_count = 0;
            while (true)
            {
                try
                {
                    exams_count = Int32.Parse(Console.ReadLine() ?? "0");
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input correct exams count:\t");
                }
            }
            for (int i = 0; i < exams_count; i++)
            {
                var exam = new Exam();
                exam.AddFromConsole();
                m_exams.Add(exam);
            }



            Console.WriteLine("Input tests count:\t");
            int tests_count = 0;
            while (true)
            {
                try
                {
                    tests_count = Int32.Parse(Console.ReadLine() ?? "0");
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input correct tests count:\t");
                }
            }
            for (int i = 0; i < tests_count; i++)
            {
                var test = new Test();
                test.AddFromConsole();
                m_tests.Add(test);
            }

            return true;
        }


        public IEnumerator GetEnumerator()
        {
            return new StudentEnumerator(m_exams, m_tests);
        }


        public IEnumerable<object> GetPassedTestsAndExams()
        {
            foreach (Test test in m_tests)
            {
                if (test.IsPassedTest == true)
                {
                    yield return test;
                }
            }
            foreach (Exam exam in m_exams)
            {
                if (exam.Mark > 2)
                {
                    yield return exam;
                }
            }
        }


        public IEnumerable<Test> GetPassedTests()
        {
            foreach (Test test in m_tests)
            {
                Exam? exam = m_exams.Find(exam => exam.Subject == test.SubjectName);
                if (exam != null && test.IsPassedTest == true && exam.Mark > 2)
                {
                    yield return test;
                }
            }
        }


        public void sortExamsBySubjectName() => m_exams.Sort();


        public void sortExamsByMark() => m_exams.Sort(new Exam());


        public void sortExamsByDate() => m_exams.Sort(new ExamComparer());



        public event PropertyChangedEventHandler? PropertyChanged;


        private void NotifyPropertyChanged(string _field_name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_field_name));
        }
    }
}
