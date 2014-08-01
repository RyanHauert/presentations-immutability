using System.Collections.Immutable;

namespace Immutability.Demo
{
    public class EventAggregator
    {
        private ImmutableList<object> _listeners = ImmutableList<object>.Empty;

        public void AddListener<T>(IListener<T> listener)
        {
        }

        public void RemoveListener<T>(IListener<T> listener)
        {
        }

        public void Publish<T>(T message)
        {
        }
    }

    public interface IListener<T>
    {
        void Handle(T message);
    }
}