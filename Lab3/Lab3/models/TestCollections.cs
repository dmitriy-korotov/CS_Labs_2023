using System;
using System.Collections.Generic;



namespace Models
{
    class TestCollections<TKey, TValue>
    {
        private List<TKey> m_list_keys = new();
        private List<string> m_strings = new();
        private Dictionary<TKey, TValue> m_dict = new();
        private Dictionary<string, TValue> m_dict_2 = new();
        private GenerateElement<TKey, TValue> m_generator;


        public TestCollections(int _elements_count, GenerateElement<TKey, TValue> _generator)
        {
            m_generator = _generator;

            for (int i = 0; i < _elements_count; i++)
            {
                KeyValuePair<TKey, TValue> pair = m_generator(i);
                m_list_keys.Add(pair.Key);
                m_dict.Add(pair.Key, pair.Value);
            }
        }



        public string FindInStringsList(string _value)
        {
            return m_strings.Find(str => str == _value) ?? "Not found";
        }



        public TKey? FindInKeysList(TKey _value)
        {
            return m_list_keys.Find(key => key.Equals(_value));
        }
    }
}
