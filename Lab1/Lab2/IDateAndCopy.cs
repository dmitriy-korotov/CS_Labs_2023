namespace Lab2
{
    internal interface IDateAndCopy
    {
        public object DeepCopy();

        public DateTime Date { get; set; }
    }
}
