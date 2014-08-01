using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using FubuCore;

namespace Immutability.Examples
{
    public class Cache<TKey, TValue> : IEnumerable<TValue>
    {
        private readonly Func<TKey, TValue> _onMissing =
            x => { throw new KeyNotFoundException("Key '{0}' could not be found".ToFormat(x)); };

        private ImmutableDictionary<TKey, TValue> _values;

        public Cache(IDictionary<TKey, TValue> values = null, Func<TKey, TValue> onMissing = null)
        {
            _values = values == null
                ? ImmutableDictionary<TKey, TValue>.Empty
                : ImmutableDictionary.CreateRange(values);

            if (onMissing != null)
            {
                _onMissing = onMissing;
            }
        }

        public int Count
        {
            get { return _values.Count; }
        }

        public TValue this[TKey key]
        {
            get { return ImmutableInterlocked.GetOrAdd(ref _values, key, _onMissing); }
            set { ImmutableInterlocked.AddOrUpdate(ref _values, key, value, (x, old) => value); }
        }

        public void Clear()
        {
            _values = _values.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return _values.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            TValue value;
            return ImmutableInterlocked.TryRemove(ref _values, key, out value);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _values.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}