namespace Immutability
{
    public class FunctionalList<T>
    {
        public static readonly FunctionalList<T> Empty = new FunctionalList<T>();

        public readonly T Head;
        public readonly FunctionalList<T> Tail;
        public readonly bool IsEmpty;

        private FunctionalList()
        {
            IsEmpty = true;
        }

        private FunctionalList(T head, FunctionalList<T> tail)
        {
            Head = head;
            Tail = tail;
            IsEmpty = false;
        }

        public int Count
        {
            get
            {
                if (IsEmpty)
                    return 0;

                return 1 + Tail.Count;
            }
        }

        public FunctionalList<T> Add(T item)
        {
            return new FunctionalList<T>(item, this);
        }

        public FunctionalList<T> Remove(T item)
        {
            if (IsEmpty)
                return this;

            if (Equals(item, Head))
                return Tail;

            // This tail recursion is not tail recursive.
            return Tail.Remove(item).Add(Head);
        }

        public bool Contains(T item)
        {
            // look familiar?
            if (IsEmpty)
                return false;

            if (Equals(item, Head))
                return true;

            return Tail.Contains(item);
        }
    }
}