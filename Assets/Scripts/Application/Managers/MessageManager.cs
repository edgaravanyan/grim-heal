using System;
using System.Collections.Generic;
using Core.Contracts.Messages;
using Core.Contracts.Pool;
using Core.Logger;
using Core.MessagePipe;

namespace Application.Managers
{
    /// <summary>
    /// Manages message publishers and subscribers, allowing for message publication and subscription.
    /// </summary>
    public class MessageManager : IMessageManager
    {
        private readonly Dictionary<Type, object> publishers = new();
        private readonly Dictionary<Type, object> subscribers = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageManager"/> class.
        /// </summary>
        /// <param name="messagesToRegister">Registrations for message publishers and subscribers.</param>
        public MessageManager(IEnumerable<IMessageRegistration> messagesToRegister)
        {
            RegisterMessages(messagesToRegister);
        }

        private void RegisterMessages(IEnumerable<IMessageRegistration> publishersToRegister)
        {
            foreach (var registration in publishersToRegister)
            {
                var messageType = registration.MessageType;
                var success = publishers.TryAdd(messageType, registration.Publisher);
                if (!success)
                {
                    LoggerProvider.DebugLogger.Warn($"A publisher for message type {messageType.Name} is already registered.");
                }
                success = subscribers.TryAdd(messageType, registration.Subscriber);
                if (!success)
                {
                    LoggerProvider.DebugLogger.Warn($"A subscriber for message type {messageType.Name} is already registered.");
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Publishes a message of type <typeparamref name="TMessage"/> with the provided data.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to publish.</typeparam>
        /// <param name="data">The data to be published with the message.</param>
        public void Publish<TMessage>(object data = null) where TMessage : class, IPoolable
        {
            var messageType = typeof(TMessage);
            if (publishers.TryGetValue(messageType, out var publisher))
            {
                ((PoolableMessagePublisher<TMessage>)publisher).Publish(data);
            }
            else
            {
                LoggerProvider.DebugLogger.Warn($"No publisher found for message type {messageType.Name}.");
            }
        }

        /// <summary>
        /// Subscribes a handler to receive messages of type <typeparamref name="TMessage"/>.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to subscribe to.</typeparam>
        /// <param name="handler">The handler to be invoked when a message of the specified type is received.</param>
        public void Subscribe<TMessage>(Action<TMessage> handler) where TMessage : class
        {
            var messageType = typeof(TMessage);
            if (subscribers.TryGetValue(messageType, out var subscriber))
            {
                ((IMessageSubscriber<TMessage>)subscriber).Subscribe(handler);
            }
            else
            {
                LoggerProvider.DebugLogger.Warn($"No subscriber found for message type {messageType.Name}.");
            }
        }
    }
}
