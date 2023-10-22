using System.Collections.Generic;

namespace Models
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    delegate string GenerateElement(int j);
    delegate TKey KeySelector<TKey>(Student _student);
}
