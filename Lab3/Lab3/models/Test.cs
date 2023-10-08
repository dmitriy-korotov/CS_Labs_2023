namespace Models
{
    internal class Test
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


        public override string ToString()
        { 
            return $"Subject name: {m_subject_name}; Is passed test: {m_is_passed_test}";
        }
    }
}
