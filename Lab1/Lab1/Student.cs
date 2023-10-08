namespace Lab1
{
    internal class Student
    {
        private Person m_person;
        private Education m_education_type;
        private int m_number_of_group;
        private Exam[] m_exams;



        public Person Person
        {
            get => m_person; 
            set => m_person = value; 
        }


        public Education EducationType
        {
            get => m_education_type; 
            set => m_education_type = value; 
        }


        public int NumberOfGroup
        {
            get => m_number_of_group; 
            set => m_number_of_group = value;
        }


        public Exam[] Exams
        {
            get => m_exams;
            set => m_exams = value;
        }


        public double AverageMark
        {
            get
            {
                if (m_exams.Length == 0)
                {
                    return 0;
                }

                double sum = 0;
                for (int i = 0; i < m_exams.Length; i++)
                {
                    sum += m_exams[i].Mark;
                }
                return sum / m_exams.Length;
            }
        }


        public bool this[Education _education_type]
        {
            get => m_education_type == _education_type;
        }


        public Student(Person _person, Education _education_type, int _number_of_group)
        {
            m_person = _person;
            m_education_type = _education_type;
            m_number_of_group = _number_of_group;
            m_exams = new Exam[0];
        }


        public Student(): this(new Person(), Education.Bachelor, 22)
        { }


        public override string ToString()
        {
            string result_string = $"Student: {m_person}\nEducation type: {m_education_type}\nExams list:\n";
            foreach (Exam exam in m_exams)
            {
                result_string += $"{exam}\n";
            }
            return result_string;
        }


        public virtual string ToShortString()
        {
            string result_string = $"Student: {m_person}\nEducation type: {m_education_type}\nAvarege mark: {AverageMark}\n";
            return result_string;
        }


        public void AddExams(params Exam[] _exams)
        {
            for (int i = 0; i < _exams.Length; i++)
            {
                m_exams = m_exams.Append(_exams[i]).ToArray();
            }
        }
    }
}
