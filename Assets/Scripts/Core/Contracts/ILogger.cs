namespace Assets.Scripts.Core.Contracts
{
    /// <summary>
    /// Represents a logger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        void Log(string message);
    }
}