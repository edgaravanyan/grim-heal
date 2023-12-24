using System.Collections.Generic;
using Core.Contracts.Messages;
using Core.Map;
using Core.MessagePipe.Messages;
using Data;
using Unity.Mathematics;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Presentation.Managers
{
    /// <summary>
    /// Manages the visual representation of the map in the Unity environment.
    /// </summary>
    public class MapManager : MonoBehaviour, IInitializable
    {
        [Inject] private Map map;
        [Inject] private IMessageManager messageManager;
        [Inject] private PrefabProvider prefabProvider;

        private readonly List<GameObject> mapChunks = new();

        /// <summary>
        /// Initializes the MapManager and sets up the initial state.
        /// </summary>
        async void IInitializable.Initialize()
        {
            var chunkPrefabs = await prefabProvider.GetMapChunksAsync();
            
            
            // Create map chunk views based on the count and positions of MapChunks in Map.
            foreach (var mapChunk in map.MapGrid.MapChunks)
            {
                var prefabIndex = Random.Range(0, chunkPrefabs.Count);
                var position = new Vector2(mapChunk.Position.X, mapChunk.Position.Y);
                
                var mapChunkGameObject = Instantiate(chunkPrefabs[prefabIndex], position, quaternion.identity);
                mapChunks.Add(mapChunkGameObject);
            }
                

            // Subscribe to MapChunkMessage events to update the visual representation when a map chunk changes.
            messageManager.Subscribe<MapChunkMessage>(message =>
            {
                var mapChunk = message.Data;
                var chunkPosition = mapChunk.Position;

                // Update the position of the corresponding map chunk view.
                mapChunks[mapChunk.Id].transform.position = new Vector2(chunkPosition.X, chunkPosition.Y);
            });
        }
    }
}