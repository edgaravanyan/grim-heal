using UnityEngine.Pool;

namespace Application.Utils
{
    /// <summary>
    /// Adapter for Unity's ObjectPool implementing IObjectPool interface.
    /// </summary>
    /// <typeparam name="T">The type of objects to pool.</typeparam>
    public class ObjectPoolAdapter<T> : Assets.Scripts.Core.Utils.Pool.IObjectPool<T> where T : class, new()
    {
        private readonly ObjectPool<T> pool = new(() => new T());

        /// <summary>
        /// Gets an object from the pool.
        /// </summary>
        /// <returns>The retrieved object.</returns>
        public T Get()
        {
            return pool.Get();
        }

        /// <summary>
        /// Releases an object back to the pool.
        /// </summary>
        /// <param name="pooledObject">The object to release.</param>
        public void Release(T pooledObject)
        {
            pool.Release(pooledObject);
        }
    }
}