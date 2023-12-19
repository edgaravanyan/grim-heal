using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data.Character;
using Data.Loaders;
using UnityEngine;
using VContainer.Unity;

namespace Data
{
    /// <summary>
    /// Class responsible for providing data and initializing character-related information.
    /// </summary>
    public class DataProvider : IInitializable
    {
        private IList<ScriptableObject> scriptableData;

        async void IInitializable.Initialize()
        {
            scriptableData = await DataLoader.LoadDataAsync();
        }

        /// <summary>
        /// Asynchronously retrieves character stats from the loaded data.
        /// </summary>
        /// <returns>The character stats.</returns>
        public async UniTask<CharacterStats> GetCharacterStatsAsync()
        {
            await UniTask.WaitWhile(() => scriptableData == null);

            foreach (var scriptableObject in scriptableData)
            {
                if (scriptableObject is CharacterStatsScriptableObject statsData)
                {
                    return new CharacterStats(statsData);
                }
            }

            return null;
        }
    }
}