using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Contracts.Map;
using Cysharp.Threading.Tasks;
using Data;
using Unity.Mathematics;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Presentation.Map
{
    /// <summary>
    /// Unity MonoBehaviour responsible for providing map view instances.
    /// </summary>
    public class MapChunkViewProvider : MonoBehaviour, IViewProvider<IMapView>
    {
        [Inject] private PrefabProvider prefabProvider;
        private IList<GameObject> mapChunks;

        /// <summary>
        /// Asynchronous initialization method called on Start.
        /// </summary>
        private async void Start()
        {
            mapChunks = await prefabProvider.GetMapChunksAsync();
        }

        /// <summary>
        /// Asynchronously creates a map view instance.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is the created map view.</returns>
        public async Task<IMapView> CreateViewAsync()
        {
            // Wait until mapChunks is initialized.
            await UniTask.WaitWhile(() => mapChunks == null);
        
            // Select a random prefab from the loaded mapChunks.
            var prefabIndex = Random.Range(0, mapChunks.Count);
            var mapChunkGameObject = Instantiate(mapChunks[prefabIndex], Vector3.zero, quaternion.identity);
        
            // Retrieve and return the IMapView component from the instantiated GameObject.
            return mapChunkGameObject.GetComponent<IMapView>();
        }
    }
}