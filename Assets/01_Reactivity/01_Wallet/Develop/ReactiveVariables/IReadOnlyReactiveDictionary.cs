using System;
using System.Collections.Generic;

public interface IReadOnlyReactiveDictionary<TKey, TValue>
{
    event Action<TKey, TValue> Changed;

    IReadOnlyDictionary<TKey, TValue> Values { get; }

    bool ContainsKey(TKey key);

    TValue GetValueOrDefault(TKey key);
}