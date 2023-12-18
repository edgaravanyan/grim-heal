using Assets.Scripts.Core.Contracts;
using MessagePipe;
using VContainer;

namespace Application.Messages
{
    /// <summary>
    /// Implementation of IMessagePublisher using MessagePipe for a specific message type.
    /// </summary>
    /// <typeparam name="T">The type of message to be published.</typeparam>
    public class MessagePublisher<T> : IMessagePublisher<T> where T : class
    {
        [Inject] private IPublisher<T> publisher;

        /// <summary>
        /// Publishes a message using the underlying MessagePipe publisher.
        /// </summary>
        /// <param name="message">The message to be published.</param>
        public void Publish(T message)
        {
            publisher.Publish(message);
        }
    }
}