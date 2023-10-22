using System;
using Interfaces;

namespace Models
{
    public class Person : IDateAndCopy
    {
        protected string m_name;
        protected string m_surname;
        protected DateTime m_birthday_date;


        public string Name
        {
            get => m_name;
            set => m_name = value;
        }


        public string Surame
        {
            get => m_surname;
            set => m_surname = value;
        }

        public DateTime Date
        {
            get => m_birthday_date;
            set => m_birthday_date = value;
        }


        public int YearOfBirthday
        {
            get => m_birthday_date.Year;
            set => m_birthday_date = new DateTime(value, m_birthday_date.Month, m_birthday_date.Day);
        }


        public Person(string _name, string _surname, DateTime _birthday_date)
        {
            m_name = _name;
            m_surname = _surname;
            m_birthday_date = _birthday_date;
        }


        public Person() : this("Dmitriy", "Korotov", new DateTime(2004, 4, 21))
        { }


        public override string ToString()
        {
            return $"{m_surname} {m_name}: {m_birthday_date.ToShortDateString()}";
        }


        public virtual string ToShortString()
        {
            return $"{m_surname} {m_name}";
        }


        public virtual object DeepCopy()
        {
            Person copy = new Person(m_name, m_surname, m_birthday_date);
            return copy;
        }


      /*  public static bool operator==(Person _left, Person _right)
        {
            return _left.ToString() == _right.ToString();
        }


        public static bool operator !=(Person _left, Person _right)
        {
            return _left.ToString() != _right.ToString();
        }*/


        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            return obj.ToString() == ToString();
        }


        public void AddFromConsole()
        {
            Console.WriteLine("Input name:\t");
            m_name = Console.ReadLine() ?? "";
            Console.WriteLine("Input surname:\t");
            m_surname = Console.ReadLine() ?? "";

            Console.WriteLine("Input birthday date:\t");
            while (true)
            {
                try
                {
                    m_birthday_date = DateTime.Parse(Console.ReadLine() ?? "24.10.2023");
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input correct dirthday date:\t");
                }
            }
        }


        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
