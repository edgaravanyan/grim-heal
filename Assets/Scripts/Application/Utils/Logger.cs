using UnityEngine;
using ILogger = Assets.Scripts.Core.Contracts.ILogger;

namespace Application.Utils
{
    /// <summary>
    /// Implementation of the ILogger interface using Unity's Debug.Log and Debug.LogWarning.
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// Logs a message using Unity's Debug.Log.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Log(string message)
        {
            Debug.Log(message);
        }

        /// <summary>
        /// Logs a warning message using Unity's Debug.LogWarning.
        /// </summary>
        /// <param name="warningMessage">The warning message to be logged.</param>
        public void Warn(string warningMessage)
        {
            Debug.LogWarning(warningMessage);
        }
    }
}