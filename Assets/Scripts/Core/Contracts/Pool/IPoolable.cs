namespace Assets.Scripts.Core.Contracts.Pool
{
    /// <summary>
    /// Represents an interface for poolable objects that require initialization.
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// Initializes the poolable object with the provided data.
        /// </summary>
        /// <param name="data">The data used for initialization.</param>
        void Initialize(object data);
    }
}