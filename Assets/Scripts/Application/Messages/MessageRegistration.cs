using System;
using Core.Contracts.Messages;
using Core.Contracts.Pool;
using Core.MessagePipe;
using VContainer;

namespace Application.Messages
{
    /// <summary>
    /// Represents the registration information for a specific message type using VContainer for dependency injection.
    /// </summary>
    /// <typeparam name="TMessage">The type of message associated with the registration.</typeparam>
    public class MessageRegistration<TMessage> : IMessageRegistration where TMessage : class, IMessage
    {
        [Inject] private PoolableMessagePublisher<TMessage> publisher;
        [Inject] private IMessageSubscriber<TMessage> subscriber;

        /// <summary>
        /// Gets the type of the message associated with this registration.
        /// </summary>
        public Type MessageType => typeof(TMessage);

        /// <summary>
        /// Gets the instance of the message publisher associated with this registration.
        /// </summary>
        public object Publisher => publisher;

        /// <summary>
        /// Gets the instance of the message subscriber associated with this registration.
        /// </summary>
        public object Subscriber => subscriber;
    }
}