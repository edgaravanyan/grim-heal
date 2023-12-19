using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data.Loaders
{
    /// <summary>
    /// Utility class for loading ScriptableObject data using Addressable Assets system.
    /// </summary>
    public static class DataLoader
    {
        private const string DataLabel = "Data";
        private const string CharacterLabel = "Character";
        
        /// <summary>
        /// Load ScriptableObject data labeled with "Data".
        /// </summary>
        /// <returns>A list of loaded ScriptableObjects.</returns>
        public static async UniTask<IList<ScriptableObject>> LoadDataAsync()
        {
            var operationHandle = Addressables.LoadAssetsAsync<ScriptableObject>(DataLabel, null);
            await operationHandle.Task;
            return operationHandle.Result;
        }
        
        /// <summary>
        /// Load ScriptableObject data labeled with "Data" and "Character".
        /// </summary>
        /// <returns>A list of loaded ScriptableObjects.</returns>
        public static async UniTask<IList<ScriptableObject>> LoadCharacterDataAsync()
        {
            var labels = new List<string> { DataLabel, CharacterLabel };
            var operationHandle = Addressables.LoadAssetsAsync<ScriptableObject>(labels, null, Addressables.MergeMode.Union);
            await operationHandle.Task;
            return operationHandle.Result;
        }
    }
}