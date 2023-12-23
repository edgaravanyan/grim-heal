using Core.Contracts.Messages;

namespace Core.MessagePipe.Messages
{
    /// <summary>
    /// A base class for implementing poolable messages.
    /// </summary>
    /// <typeparam name="TData">The type of message data.</typeparam>
    public abstract class Message<TData> : IMessage
    {
        public TData Data { get; private set; }
        
        
        /// <summary>
        /// Initializes the message with the provided data.
        /// </summary>
        /// <param name="data">The data to initialize the message.</param>
        public void Initialize(object data)
        {
            if (data != null) Data = (TData)data;
        }
    }
}