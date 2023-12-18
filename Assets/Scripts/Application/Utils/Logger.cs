using UnityEngine;
using ILogger = Assets.Scripts.Core.Contracts.ILogger;

namespace Application.Utils
{
    /// <summary>
    /// Implementation of the ILogger interface using Unity's Debug.Log.
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
    }
}