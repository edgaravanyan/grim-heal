using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Data.Loaders
{
    /// <summary>
    /// Utility class for loading ScriptableObject data and GameObject prefabs using Addressable Assets system.
    /// </summary>
    public static class DataLoader
    {
        private const string DataLabel = "Data";
        private const string PrefabLabel = "Prefab";
        
        private const string MapLabel = "Map";
        private const string CharacterLabel = "Character";

        /// <summary>
        /// Asynchronously loads ScriptableObject data labeled with "Data".
        /// </summary>
        /// <returns>An asynchronous task containing a list of loaded ScriptableObjects.</returns>
        public static async UniTask<IList<ScriptableObject>> LoadScriptableDataAsync()
        {
            return await LoadDataAsync<ScriptableObject>(new List<string> { DataLabel });
        }

        /// <summary>
        /// Asynchronously loads GameObject prefabs labeled with "Prefab" and "Map".
        /// </summary>
        /// <returns>An asynchronous task containing a list of loaded GameObject prefabs.</returns>
        public static async UniTask<IList<GameObject>> LoadMapPrefabsAsync()
        {
            return await LoadDataAsync<GameObject>(new List<string> { PrefabLabel, MapLabel });
        }

        /// <summary>
        /// Asynchronously loads assets of type TData based on the provided labels.
        /// </summary>
        /// <typeparam name="TData">The type of assets to load (e.g., ScriptableObject or GameObject).</typeparam>
        /// <param name="labels">The labels used to identify the assets.</param>
        /// <returns>An asynchronous task containing a list of loaded assets of type TData.</returns>
        private static async UniTask<IList<TData>> LoadDataAsync<TData>(IEnumerable<string> labels) where TData : Object
        {
            return await Addressables.LoadAssetsAsync<TData>(
                labels,
                null,
                Addressables.MergeMode.Union);
        }
    }
}
