using Assets.Scripts.Core.Contracts;

namespace Assets.Scripts.Core.Logger
{
    /// <summary>
    /// Provides a static access point for an ILogger instance.
    /// </summary>
    public static class LoggerProvider
    {
        /// <summary>
        /// Gets or sets the ILogger instance.
        /// </summary>
        public static ILogger DebugLogger { get; private set; }

        /// <summary>
        /// Initializes the LoggerProvider with an ILogger instance.
        /// </summary>
        /// <param name="logger">The ILogger instance to be set.</param>
        public static void Initialize(ILogger logger)
        {
            DebugLogger = logger;
        }
    }
}