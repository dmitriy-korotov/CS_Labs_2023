using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    internal class StudentEnumerator : System.Collections.IEnumerator
    {
        IEnumerable<string> m_subjects;
        int m_current_index;

        public bool MoveNext()
        {
            m_current_index++;
            if (m_subjects.Count() <= m_current_index)
            {
                return false;
            }
            return true;
        }


        public object Current
        {
            get { return m_subjects.ElementAt(m_current_index); }
        }


        public void Reset()
        {
            m_current_index = -1;
        }


        public StudentEnumerator(List<Exam> _exams, List<Test> _tests)
        {
            
            var subjects_from_exams = _exams.Select(exam => exam.Subject);
            var subjects_from_tests = _tests.Select(test => test.SubjectName);

            m_subjects = subjects_from_exams.Intersect(subjects_from_tests);

            m_current_index = -1;
        }
    }
}
