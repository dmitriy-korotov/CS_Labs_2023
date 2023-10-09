using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class StudentCollection<TKey>
    {
        private readonly Dictionary<TKey, Student> m_students = new();
        private KeySelector<TKey> m_key_selector;


        public double MaxAverageMark
        {
            get
            {
                if (m_students.Count == 0)
                    return 0;

                double max = m_students.Max((student) =>
                {
                    return student.Value.Exams.Average((exam) => exam.Mark);
                });
                return max;
            }
        }



        public IEnumerable<IGrouping<Education, KeyValuePair<TKey, Student>>> GroupByEducation
        {
            get
            {
                return m_students.GroupBy((student) => student.Value.EducationType);
            }
        }



        public StudentCollection(KeySelector<TKey> _selector)
        {
            m_key_selector = _selector;
        }


        public void AddDefault(int _students_count)
        {
            for (int i = 0; i < _students_count; i++)
            {
                Student student = new Student();
                m_students.Add(m_key_selector(student), student);
            }
        }


        public void AddStudents(params Student[] _students)
        {
            foreach (var student in _students)
            {
                m_students.Add(m_key_selector(student), student);
            }
        }


        public IEnumerable<KeyValuePair<TKey, Student>> EducationForm(Education _value)
        {
            return m_students.Where((student) => student.Value.EducationType == _value);
        }



        public override string ToString()
        {
            string result = "Students:\n";

            for (int i = 0; i < m_students.Count; i++)
            {
                result += $"\nStudent {i + 1}:\n{m_students.Values.ElementAt(i)}";
            }
            return result;
        }
    }
}
