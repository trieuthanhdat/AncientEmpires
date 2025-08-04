

using System;
using System.Collections;
using System.Collections.Generic;

public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
{
	private readonly IDictionary<TKey, TValue> _dictionary;

	TValue IDictionary<TKey, TValue>.this[TKey key]
	{
		get
		{
			return this[key];
		}
		set
		{
			throw ReadOnlyException();
		}
	}

	public ICollection<TKey> Keys => _dictionary.Keys;

	public ICollection<TValue> Values => _dictionary.Values;

	public TValue this[TKey key] => _dictionary[key];

	public int Count => _dictionary.Count;

	public bool IsReadOnly => true;

	public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
	{
		_dictionary = dictionary;
	}

	void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
	{
		throw ReadOnlyException();
	}

	public bool ContainsKey(TKey key)
	{
		return _dictionary.ContainsKey(key);
	}

	bool IDictionary<TKey, TValue>.Remove(TKey key)
	{
		throw ReadOnlyException();
	}

	public bool TryGetValue(TKey key, out TValue value)
	{
		return _dictionary.TryGetValue(key, out value);
	}

	void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
	{
		throw ReadOnlyException();
	}

	void ICollection<KeyValuePair<TKey, TValue>>.Clear()
	{
		throw ReadOnlyException();
	}

	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		return _dictionary.Contains(item);
	}

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		_dictionary.CopyTo(array, arrayIndex);
	}

	bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
	{
		throw ReadOnlyException();
	}

	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		return _dictionary.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private static Exception ReadOnlyException()
	{
		return new NotSupportedException("This dictionary is read-only");
	}
}
