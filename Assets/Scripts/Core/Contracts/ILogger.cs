namespace Assets.Scripts.Core.Contracts
{
    /// <summary>
    /// Represents a logger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a general message.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        void Log(string message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="warningMessage">The warning message to be logged.</param>
        void Warn(string warningMessage);
    }
}