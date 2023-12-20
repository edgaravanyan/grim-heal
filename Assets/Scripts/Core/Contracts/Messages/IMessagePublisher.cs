namespace Assets.Scripts.Core.Contracts.Messages
{
    /// <summary>
    /// Represents a message publisher interface for a specific message type.
    /// </summary>
    /// <typeparam name="T">The type of message to be published.</typeparam>
    public interface IMessagePublisher<in T> where T : class
    {
        /// <summary>
        /// Publishes a message of the specified type.
        /// </summary>
        /// <param name="message">The message to be published.</param>
        void Publish(T message);
    }
}