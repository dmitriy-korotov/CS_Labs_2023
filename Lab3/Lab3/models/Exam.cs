using System;
using System.Collections.Generic;
using Interfaces;

namespace Models
{
    internal class Exam : IDateAndCopy, IComparable, IComparer<Exam>
    {
        private string m_subject;
        private int m_mark;
        private DateTime m_starting_date;


        public string Subject
        {
            get => m_subject;
            set => m_subject = value;
        }


        public DateTime Date
        {
            get => m_starting_date;
            set => m_starting_date = value;
        }


        public int Mark
        {
            get => m_mark;
            set => m_mark = value;
        }


        public Exam(string _subject, int _mark, DateTime _starting_date)
        {
            m_subject = _subject;
            m_mark = _mark;
            m_starting_date = _starting_date;
        }



        public Exam() : this("Math", 5, new DateTime(2024, 1, 15))
        { }



        public override string ToString()
        {
            return $"Subject: {m_subject}; mark: {m_mark}; starting date: {m_starting_date.ToShortDateString()}";
        }



        public object DeepCopy() 
        {
            Exam copy = new Exam(m_subject, m_mark, m_starting_date);
            return copy;
        }


        public int CompareTo(object obj)
        {
            Exam ex = obj as Exam ?? throw new ArgumentException("Object is null");
            if (ex != null)
                return Subject.CompareTo(ex.Subject);
            else
                throw new ArgumentException("Object is not a Exam");
        }


        public int Compare(Exam left, Exam right)
        {
            return left.m_mark.CompareTo(right.m_mark);
        }
    }
}
