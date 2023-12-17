namespace Assets.Scripts.Core.Utils.Pool
{
    /// <summary>
    /// Represents an interface for poolable objects that require initialization.
    /// </summary>
    public interface IPoolable<in T>
    {
        /// <summary>
        /// Initializes the poolable object with the provided data.
        /// </summary>
        /// <param name="data">The data used for initialization.</param>
        void Initialize(T data);
    }
}