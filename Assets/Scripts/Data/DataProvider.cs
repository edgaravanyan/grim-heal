using System.Collections.Generic;
using System.Linq;
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
            scriptableData = await DataLoader.LoadScriptableDataAsync();
        }

        /// <summary>
        /// Asynchronously retrieves character stats from the loaded data.
        /// </summary>
        /// <returns>The character stats.</returns>
        public async UniTask<CharacterData> GetCharacterDataAsync()
        {
            await UniTask.WaitWhile(() => scriptableData == null);

            var statsData = scriptableData.First(scriptableObject => scriptableObject is CharacterDataScriptableObject);
            return new CharacterData((CharacterDataScriptableObject)statsData);
        }
    }
}