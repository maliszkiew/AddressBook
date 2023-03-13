using System.Collections.Generic;
using System;

namespace AddressBook.Models
{
    public class EventAggregator
    {
        private Dictionary<Type, Action<object>> _subscriptions = new Dictionary<Type, Action<object>>();

        public void Subscribe<T>(Action<T> action)
        {
            _subscriptions[typeof(T)] = obj =>
            {
                T tObj = (T)obj;
                action(tObj);
            };
        }
        public void Unsubscribe<T>()
        {
            _subscriptions.Remove(typeof(T));
        }
        public void Publish<T>(T message)
        {
            if (_subscriptions.TryGetValue(typeof(T), out var action)) //if message of this type exists, its action is assigned to var action
            {
                action(message);
            }
        }
    }
    public class JsonChangedMessage
    {
    }
}