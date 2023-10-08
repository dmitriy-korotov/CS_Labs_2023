using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class StudentCollection<TKey>
    {
        Dictionary<TKey, Student> m_students = new();
        KeySelector<TKey> m_key_selector;


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
