using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Immutability
{
    public class SlowImmutableList<T> : IEnumerable<T>
    {
        private readonly IList<T> _inner = new List<T>();

        public SlowImmutableList()
        {
        }

        public SlowImmutableList(IList<T> list)
        {
            _inner = list;
        }

        public int Count
        {
            get { return _inner.Count; }
        }

        public SlowImmutableList<T> Add(T item)
        {
            var copy = _inner.ToList();
            copy.Add(item);
            return new SlowImmutableList<T>(copy);
        }

        public SlowImmutableList<T> Remove(T item)
        {
            var copy = _inner.ToList();
            copy.Remove(item);
            return new SlowImmutableList<T>(copy);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}