using System;

namespace Interfaces
{
    internal interface IDateAndCopy
    {
        public object DeepCopy();

        public DateTime Date { get; set; }
    }
}
