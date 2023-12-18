using Assets.Scripts.Core.Contracts.Pool;
using MessagePipe;
using VContainer;

namespace Assets.Scripts.Core.MessagePipe
{
    /// <summary>
    /// Provides a wrapper around IPublisher for publishing poolable messages and ensuring proper disposal.
    /// </summary>
    /// <typeparam name="T">The type of the poolable message.</typeparam>
    /// <typeparam name="TU">The type of the data to initialize the poolable message.</typeparam>
    public class PoolableMessagePublisher<T, TU> where T : class, IPoolable<TU>
    {
        [Inject] private IObjectPool<T> messagePool;
        [Inject] private IPublisher<T> publisher;

        /// <summary>
        /// Publishes the poolable message and disposes of it.
        /// </summary>
        /// <param name="data">The data to initialize the poolable message.</param>
        public void Publish(TU data)
        {
            var message = messagePool.Get();
            message.Initialize(data);
            publisher.Publish(message);
            messagePool.Release(message);
        }
    }
}