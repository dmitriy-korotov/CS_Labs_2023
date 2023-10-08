using System;
using System.Collections.Generic;



namespace Models
{
    class ExamComparer : IComparer<Exam>
    {
        public int Compare(Exam left, Exam right)
        {
            return left.Date.CompareTo(right.Date);
        }
    }
}
