using System;

namespace Assets.Scripts.Core.Contracts.Messages
{
    /// <summary>
    /// Represents a message subscriber interface for a specific message type.
    /// </summary>
    /// <typeparam name="T">The type of message to subscribe to.</typeparam>
    public interface IMessageSubscriber<out T> where T : class
    {
        /// <summary>
        /// Subscribes to messages of the specified type.
        /// </summary>
        /// <param name="handler">The handler to be invoked when a message of the specified type is received.</param>
        void Subscribe(Action<T> handler);
    }
}