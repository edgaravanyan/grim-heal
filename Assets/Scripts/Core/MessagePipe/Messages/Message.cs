using Assets.Scripts.Core.Utils.Pool;

namespace Assets.Scripts.Core.MessagePipe.Messages
{
    /// <summary>
    /// A base class for implementing poolable messages.
    /// </summary>
    /// <typeparam name="T">The concrete message type.</typeparam>
    public abstract class Message<T> : IPoolable<T>
    {
        public T data { get; private set; }
        
        
        /// <summary>
        /// Initializes the message with the provided data.
        /// </summary>
        /// <param name="data">The data to initialize the message.</param>
        public void Initialize(T data)
        {
            this.data = data;
        }
    }
}