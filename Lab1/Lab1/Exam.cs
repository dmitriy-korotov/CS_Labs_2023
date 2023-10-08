﻿namespace Lab1
{
    internal class Exam
    {
        private string m_subject;
        private int m_mark;
        private DateTime m_starting_date;


        public string Subject
        {
            get => m_subject; 
            set => m_subject = value; 
        }



        public int Mark
        {
            get => m_mark; 
            set => m_mark = value;
        }


        public DateTime StartingDate
        {
            get => m_starting_date; 
            set => m_starting_date = value;
        }



        public Exam(string _subject, int _mark, DateTime _starting_date)
        {
            m_subject = _subject;
            m_mark = _mark;
            m_starting_date = _starting_date;
        }



        public Exam(): this("Math", 5, new DateTime(2024, 1, 15))
        { }



        public override string ToString()
        {
            return $"Subject: {m_subject}; mark: {m_mark}; starting date: {m_starting_date.ToShortDateString()}";
        }
    }
}
