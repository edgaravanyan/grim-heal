using System;
using Core.Contracts.Messages;
using MessagePipe;
using VContainer;

namespace Application.Messages
{
    /// <summary>
    /// Implementation of IMessageSubscriber using MessagePipe for a specific message type.
    /// </summary>
    /// <typeparam name="T">The type of message to which this subscriber is subscribed.</typeparam>
    public class MessageSubscriber<T> : IMessageSubscriber<T> where T : class
    {
        [Inject] private ISubscriber<T> subscriber;

        /// <summary>
        /// Subscribes a handler to receive messages of the specified type.
        /// </summary>
        /// <param name="handler">The handler to be invoked when a message of the specified type is received.</param>
        public void Subscribe(Action<T> handler)
        {
            subscriber.Subscribe(handler);
        }
    }
}