using System.Xml.Linq;

namespace Models
{
    public class Test
    {
        private string m_subject_name;
        private bool m_is_passed_test;


        public string SubjectName
        {
            get => m_subject_name;
            set => m_subject_name = value;
        }


        public bool IsPassedTest
        {
            get => m_is_passed_test;
            set => m_is_passed_test = value;
        }


        public Test(string subject_name, bool is_passed_test)
        {
            m_subject_name = subject_name;
            m_is_passed_test = is_passed_test;
        }


        public Test(): this("Math", true)
        { }


        public void AddFromConsole()
        {
            Console.WriteLine("Input subject name:\t");
            m_subject_name = Console.ReadLine() ?? "";

            Console.WriteLine("Is passed test (YES or NO):\t");
            while(true)
            {
                string passed_str = Console.ReadLine() ?? "";
                if (passed_str == "YES")
                {
                    m_is_passed_test = true;
                    break;
                }
                if (passed_str == "NO")
                {
                    m_is_passed_test = false;
                    break;
                }
                Console.WriteLine("Please input correct data (YES or NO):\t");
            }
        }


        public override string ToString()
        { 
            return $"Subject name: {m_subject_name}; Is passed test: {m_is_passed_test}";
        }
    }
}
