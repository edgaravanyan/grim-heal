using System;
using MessagePipe;
using VContainer;

namespace Core.MessagePipe
{
    /// <summary>
    /// Provides a wrapper around IPublisher for publishing poolable messages and ensuring proper disposal.
    /// </summary>
    /// <typeparam name="T">The type of the poolable message.</typeparam>
    public class PoolableMessagePublisher<T> where T : IDisposable
    {
        [Inject] private IPublisher<T> publisher;

        /// <summary>
        /// Publishes the poolable message and disposes of it.
        /// </summary>
        /// <param name="message">The poolable message to publish.</param>
        public void Publish(T message)
        {
            publisher.Publish(message);
            message.Dispose();
        }
    }
}