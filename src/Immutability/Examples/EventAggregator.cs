using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Immutability.Examples
{
    public class EventAggregator
    {
        private ImmutableList<object> _listeners = ImmutableList<object>.Empty;

        public void AddListener<T>(IListener<T> listener)
        {
            // Can you spot the race condition?
            _listeners = _listeners.Add(listener);
        }

        public void RemoveListener<T>(IListener<T> listener)
        {
            _listeners = _listeners.Remove(listener);
        }

        public void Publish<T>(T message)
        {
            _listeners.OfType<IListener<T>>()
                .Each(x => x.Handle(message));
        }
    }

    public interface IListener<T>
    {
        void Handle(T message);
    }
}