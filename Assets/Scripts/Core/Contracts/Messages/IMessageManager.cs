using System;
using Core.Contracts.Pool;

namespace Core.Contracts.Messages
{
    /// <summary>
    /// Represents a message manager interface responsible for publishing and subscribing to messages.
    /// </summary>
    public interface IMessageManager
    {
        /// <summary>
        /// Publishes a message with the provided data.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to be published.</typeparam>
        /// <param name="data">The data associated with the message.</param>
        void Publish<TMessage>(object data) where TMessage : class, IPoolable;

        /// <summary>
        /// Subscribes to messages of the specified type with the provided handler.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to subscribe to.</typeparam>
        /// <param name="handler">The action to be invoked when a message is received.</param>
        void Subscribe<TMessage>(Action<TMessage> handler) where TMessage : class;
    }
}