using Core.Utils.Pool;
using UnityEngine.Pool;

namespace Core.MessagePipe.Messages
{
    /// <summary>
    /// A base class for implementing poolable messages.
    /// </summary>
    /// <typeparam name="T">The concrete message type.</typeparam>
    /// <typeparam name="TU">The type of data to initialize the message.</typeparam>
    public abstract class Message<T, TU> : IPoolable<TU> where T : Message<T, TU>, new()
    {
        /// <summary>
        /// Initializes the message with the provided data.
        /// </summary>
        /// <param name="data">The data to initialize the message.</param>
        public abstract void Initialize(TU data);

        private static readonly ObjectPool<T> Pool = new (() => new T());

        /// <summary>
        /// Gets a new instance of the message from the pool and initializes it with the provided data.
        /// </summary>
        /// <param name="data">The data to initialize the message.</param>
        /// <returns>The initialized message instance.</returns>
        public static T Get(TU data)
        {
            var message = Pool.Get();
            message.Initialize(data);
            return message;
        }

        /// <summary>
        /// Releases the message back to the pool.
        /// </summary>
        public void Dispose()
        {
            Pool.Release((T)this);
        }
    }
}