using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.models
{
    internal class JournalEntry
    {
        public string CollectionName { get; set; }
        public util.Action ActionType { get; set; }
        public string StudentProperty { get; set; }
        public string Key { get; set; }


        public JournalEntry(string collectionName, util.Action action, string studentProperty, string key)
        {
            CollectionName = collectionName;
            ActionType = action;
            StudentProperty = studentProperty;
            Key = key;
        }


        public override string ToString()
        {
            return $"Collection name: {CollectionName}\n" +
                   $"Action: {ActionType}\n" +
                   $"Student property: {StudentProperty}\n" +
                   $"Key: {Key}";
        }
    }
}
