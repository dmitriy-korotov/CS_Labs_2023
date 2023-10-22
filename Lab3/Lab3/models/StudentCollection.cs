using Lab3.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    delegate void StudentsChangedHandler<TKey> (object _source, StudentsChangedEventArgs<TKey> _args);


    class StudentCollection<TKey>
    {
        private readonly Dictionary<TKey, Student> m_students = new();
        private KeySelector<TKey> m_key_selector;


        public string CollectionName { get; set; } 


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



        public StudentCollection(string collection_name, KeySelector<TKey> _selector)
        {
            m_key_selector = _selector;
            CollectionName = collection_name;
        }


        public void AddDefault(int _students_count)
        {
            for (int i = 0; i < _students_count; i++)
            {
                Student student = new Student();
                m_students.Add(m_key_selector(student), student);
                student.PropertyChanged += PropertyToStudentChangedCast;
                StudentsChanged?.Invoke(this, new StudentsChangedEventArgs<TKey>(CollectionName, Lab3.util.Action.Add, "", m_key_selector(student)));
            }
        }


        public void AddStudents(params Student[] _students)
        {
            foreach (var student in _students)
            {
                m_students.Add(m_key_selector(student), student);
                student.PropertyChanged += PropertyToStudentChangedCast;
                StudentsChanged?.Invoke(this, new StudentsChangedEventArgs<TKey>(CollectionName, Lab3.util.Action.Add, "", m_key_selector(student)));
            }
        }


        public IEnumerable<KeyValuePair<TKey, Student>> EducationForm(Education _value)
        {
            return m_students.Where((student) => student.Value.EducationType == _value);
        }



        public bool Remove(Student _student)
        {
            bool is_deleted = m_students.Remove(m_key_selector(_student));
            if (is_deleted)
            {
                _student.PropertyChanged -= PropertyToStudentChangedCast;
                StudentsChanged?.Invoke(this, new StudentsChangedEventArgs<TKey>(CollectionName, Lab3.util.Action.Remove, "", m_key_selector(_student))); 
            }
            return is_deleted;
        }



        public override string ToString()
        {
            string result = $"Collection:\t{CollectionName}\nStudents:\n";

            for (int i = 0; i < m_students.Count; i++)
            {
                result += $"\nStudent {i + 1}:\n{m_students.Values.ElementAt(i)}";
            }
            return result;
        }



        public event StudentsChangedHandler<TKey> StudentsChanged;



        private void PropertyToStudentChangedCast(object? _sender, PropertyChangedEventArgs _args)
        {
            if (_sender as Student == null)
                return;

            StudentsChanged?.Invoke(this, new StudentsChangedEventArgs<TKey>(CollectionName, Lab3.util.Action.Property,
                                                                             _args.PropertyName, m_key_selector((Student)_sender)));
        }
    }
}
