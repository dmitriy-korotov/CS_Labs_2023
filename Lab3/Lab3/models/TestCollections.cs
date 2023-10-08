using System;
using System.Collections.Generic;



namespace Models
{
    class TestCollections<TKey, TValue>
    {
        List<TKey> m_list_keys;
        List<string> m_strings;
        Dictionary<TKey, TValue> m_dict;
        Dictionary<string, TValue> m_dict_2;
        GenerateElement<TKey, TValue> generator;


        public TestCollections(int _elements_count, GenerateElement<TKey, TValue> _generator)
        {
            for (int i = 0; i < _elements_count; i++)
            {
                KeyValuePair<TKey, TValue> pair = _generator(i);
                m_list_keys.Add(pair.Key);
                m_dict.Add(pair.Key, pair.Value);
            }
        }
    }
}
