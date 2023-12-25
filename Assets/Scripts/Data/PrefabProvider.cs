using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data.Loaders;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Provides asynchronous methods for loading prefabs
    /// </summary>
    public class PrefabProvider
    {
        /// <summary>
        /// Asynchronously retrieves a list of GameObjects representing map chunks.
        /// </summary>
        /// <returns>An asynchronous task containing the list of GameObjects representing map chunks.</returns>
        public async UniTask<IList<GameObject>> GetMapChunksAsync()
        {
            return await DataLoader.LoadMapPrefabsAsync();
        }
    }
}