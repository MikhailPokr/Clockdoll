using System;
using System.Collections.Generic;
using UnityEngine;

internal static class SignalBus
{
    private static readonly Dictionary<Type, List<Subscription>> _signalSubscriptions = new();

    private class Subscription
    {
        public object Subscriber;
        public Action<ISignal> Handler;
    }

    public static void Subscribe<T>(object subscriber, Action handler) where T : ISignal => Subscribe<T>(subscriber, _ => handler());
    public static void Subscribe<T>(object subscriber, Action<T> handler) where T : ISignal
    {
        Type signalType = typeof(T);

        if (subscriber is MonoBehaviour monobeh)
        {
            AutoUnsubscribe(monobeh);
        }

        if (!_signalSubscriptions.ContainsKey(signalType))
            _signalSubscriptions[signalType] = new List<Subscription>();

        Action<object> convertedHandler = obj => handler((T)obj);

        _signalSubscriptions[signalType].Add(new Subscription
        {
            Subscriber = subscriber,
            Handler = convertedHandler
        });

        if (subscriber is MonoBehaviour mono)
        {
            mono.gameObject.AddComponent<SignalBusAutoCleanup>().Subscriber = subscriber;
        }
    }

    public static void UnsubscribeAll(object subscriber)
    {
        foreach (var subscriptions in _signalSubscriptions.Values)
        {
            subscriptions.RemoveAll(sub => sub.Subscriber == subscriber);
        }
    }

    public static void Publish<T>(T signal) where T : ISignal
    {
        Type signalType = typeof(T);

        if (_signalSubscriptions.TryGetValue(signalType, out var subscriptions))
        {
            foreach (var sub in subscriptions.ToArray())
            {
                sub.Handler?.Invoke(signal);
            }
        }
    }

    public static void AutoUnsubscribe(this MonoBehaviour subscriber)
    {
        subscriber.gameObject.AddComponent<SignalBusAutoCleanup>().Subscriber = subscriber;
    }

    private class SignalBusAutoCleanup : MonoBehaviour
    {
        public object Subscriber { get; set; }

        private void OnDestroy()
        {
            if (Subscriber != null)
                UnsubscribeAll(Subscriber);
        }
    }
}
