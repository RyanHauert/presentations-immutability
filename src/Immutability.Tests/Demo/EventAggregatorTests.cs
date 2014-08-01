using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Immutability.Demo;
using Xunit;

namespace Immutability.Tests.Demo
{
    public class EventAggregatorTests
    {
        [Fact]
        public void CanPublishEventsWhileAddingListeners()
        {
            //var eventAggregator = new EventAggregator();
            var eventAggregator = new MutableEventAggregator();
            var addTask = Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    eventAggregator.AddListener(new TestListener());
                    Thread.Sleep(10);
                }
            });

            for (int i = 0; i < 100000; i++)
            {
                eventAggregator.Publish(new TestMessage());
            }
        }

        private class TestListener : IListener<TestMessage>
        {
            private readonly List<TestMessage> _handledMessages = new List<TestMessage>(); 
            public void Handle(TestMessage message)
            {
                _handledMessages.Add(message);
            }
        }

        private class TestMessage { }
    }

    public class MutableEventAggregator
    {
        private readonly List<object> _listeners = new List<object>();

        public void AddListener<T>(IListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener<T>(IListener<T> listener)
        {
            _listeners.Remove(listener);
        }

        public void Publish<T>(T message)
        {
            _listeners.OfType<IListener<T>>()
                .Each(x => x.Handle(message));
        }
    }
}