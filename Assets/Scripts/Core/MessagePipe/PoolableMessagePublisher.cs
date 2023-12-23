using Core.Contracts.Messages;
using Core.Contracts.Pool;

namespace Core.MessagePipe
{
    /// <summary>
    /// Provides a wrapper around IPublisher for publishing poolable messages and ensuring proper disposal.
    /// </summary>
    /// <typeparam name="TMessage">The type of the poolable message.</typeparam>
    public class PoolableMessagePublisher<TMessage> where TMessage : class, IMessage
    {
        private IObjectPool<TMessage> messagePool;
        private IMessagePublisher<TMessage> publisher;

        public PoolableMessagePublisher(IObjectPool<TMessage> messagePool, IMessagePublisher<TMessage> publisher)
        {
            this.messagePool = messagePool;
            this.publisher = publisher;
        }

        /// <summary>
        /// Publishes the poolable message and disposes of it.
        /// </summary>
        /// <param name="data">The data to initialize the poolable message.</param>
        public void Publish(object data = null)
        {
            var message = messagePool.Get();
            message.Initialize(data);
            publisher.Publish(message);
            messagePool.Release(message);
        }
    }
}