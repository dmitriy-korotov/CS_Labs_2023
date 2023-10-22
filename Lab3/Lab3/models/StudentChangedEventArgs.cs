namespace Lab3.models
{
    internal class StudentsChangedEventArgs<TKey>: EventArgs
    {
        public string CollectionName { get; set; }
        public util.Action ActionType { get; set; }
        public string StudentProperty { get; set; }
        public TKey Key { get; set; }



        public StudentsChangedEventArgs(string _collection_name, util.Action _action, string _student_property, TKey _key)
        {
            CollectionName = _collection_name;
            ActionType = _action;
            StudentProperty = _student_property;
            Key = _key;
        }


        public override string ToString()
        {
            return $"Collection name: {CollectionName}\n" +
                   $"Action: {ActionType}\n" +
                   $"Student property{StudentProperty}\n" +
                   $"Key: {Key}";
        }
    }
}
