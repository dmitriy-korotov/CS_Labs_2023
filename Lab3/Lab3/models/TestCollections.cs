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


        public List<TKey> ListKeys { get => m_list_keys; }
        public List<string> ListStrings { get => m_strings; }
        public Dictionary<TKey, TValue> Dict1 { get => m_dict; }
        public Dictionary<string, TValue> Dict2 { get => m_dict_2; }


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


        public void fillStringsList(int _elements_count, GenerateElement _strings_creator)
        {
            for (int i = 0; i < _elements_count; i++)
            {
                m_strings.Add(_strings_creator(i));
            }
        }



        public void fillDictWithKeyString(int _elements_count, GenerateElement<string, TValue> _strings_creator)
        {
            for (int i = 0; i < _elements_count; i++)
            {
                KeyValuePair<string, TValue> pair = _strings_creator(i);
                m_dict_2.Add(pair.Key, pair.Value);
            }
        }



        public string FindInStringsList(string _value)
        {
            return m_strings.Find(str => str == _value) ?? "Not found";
        }



        public TKey FindInKeysList(TKey _value) => m_list_keys.Find(key => key.Equals(_value));


        public TValue FindInDict1(TKey _key)
        {
            TValue value;
            m_dict.TryGetValue(_key, out value);
            return value;
        }


        public TValue FindInDict2(string _key)
        {
            TValue value;
            m_dict_2.TryGetValue(_key, out value);
            return value;
        }
    }
}
