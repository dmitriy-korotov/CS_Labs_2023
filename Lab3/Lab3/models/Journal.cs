using Models;

namespace Lab3.models
{
    internal class Journal
    {
        private List<JournalEntry> m_entries = new();
        


        public void StudentChangedHandler<TKey>(object _sender, StudentsChangedEventArgs<TKey> _args)
        {
            JournalEntry journal_entry = new JournalEntry(_args.CollectionName, _args.ActionType, _args.StudentProperty, _args.Key.ToString());
            m_entries.Add(journal_entry);
        }


        public override string ToString()
        {
            string result = "";

            foreach (var journal_entry in m_entries)
            {
                result += journal_entry.ToString() + "\n";
            }

            return result;
        }
    }
}
