using System;

namespace Core.Utils.Pool
{
    /// <summary>
    /// Represents an interface for poolable objects that require initialization and disposal.
    /// </summary>
    public interface IPoolable<in T> : IDisposable
    {
        /// <summary>
        /// Initializes the poolable object with the provided data.
        /// </summary>
        /// <param name="data">The data used for initialization.</param>
        void Initialize(T data);
    }
}