namespace Core.Contracts.Pool
{
    /// <summary>
    /// Represents an object pool interface.
    /// </summary>
    /// <typeparam name="T">The type of objects in the pool.</typeparam>
    public interface IObjectPool<T> where T : class
    {
        /// <summary>
        /// Retrieves an object from the pool.
        /// </summary>
        /// <returns>The retrieved object.</returns>
        T Get();

        /// <summary>
        /// Releases an object back to the pool.
        /// </summary>
        /// <param name="pooledObject">The object to be released.</param>
        void Release(T pooledObject);
    }
}