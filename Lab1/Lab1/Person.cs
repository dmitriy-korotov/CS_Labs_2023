namespace Lab1
{
    internal class Person
    {
        private string m_name;
        private string m_surname;
        private DateTime m_dirthday_date;


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

        public DateTime BirthDayDate
        {
            get => m_dirthday_date; 
            set => m_dirthday_date = value;
        }


        public int YearOfBirthday
        {
            get => m_dirthday_date.Year; 
            set => m_dirthday_date = new DateTime(value, m_dirthday_date.Month, m_dirthday_date.Day); 
        }


        public Person(string _name, string _surname, DateTime _birthday_date)
        {
            m_name = _name;
            m_surname = _surname;
            m_dirthday_date = _birthday_date;
        }


        public Person() : this("Dmitriy", "Korotov", new DateTime(2004, 4, 21))
        { }


        public override string ToString()
        {
            return $"{m_surname} {m_name} {BirthDayDate.ToShortDateString()}";
        }


        public virtual string ToShortString()
        {
            return $"{m_surname} {m_name}";
        }
    }
}
