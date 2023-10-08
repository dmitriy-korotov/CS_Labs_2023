using System.Collections;

namespace Lab2
{
    internal class Student : Person, System.Collections.IEnumerable
    {
        private Education m_education_type = new();
        private int m_number_of_group = new();
        private ArrayList m_tests = new();
        private ArrayList m_exams = new();



        public Education EducationType
        {
            get => m_education_type;
            set => m_education_type = value;
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
            }
        }


        public ArrayList Exams
        {
            get => m_exams;
            set => m_exams = value;
        }


        public ArrayList Tests
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
            copy.addExams(m_exams);
            copy.addTests(m_tests);
            return copy;
        }


        public IEnumerator GetEnumerator()
        {
            return new StudentEnumerator(m_exams.ToArray().ToList<Exam>(), m_tests);
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
                Exam? exam = m_exams(exam => exam.Subject == test.SubjectName);
                if (exam != null && test.IsPassedTest == true && exam.Mark > 2)
                {
                    yield return test;
                }
            }
        }
    }
}
