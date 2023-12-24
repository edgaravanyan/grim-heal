using System.Collections.Generic;
using Core.Contracts.Messages;
using Core.Map;
using Core.MessagePipe.Messages;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Managers
{
    /// <summary>
    /// Manages the visual representation of the map in the Unity environment.
    /// </summary>
    public class MapManager : MonoBehaviour, IInitializable
    {
        /// <summary>
        /// List of Unity Transforms representing the visual elements of map chunks.
        /// </summary>
        public List<Transform> testMapChunkView;

        [Inject] private Map map;
        [Inject] private IMessageManager messageManager;

        /// <summary>
        /// Initializes the MapManager and sets up the initial state.
        /// </summary>
        void IInitializable.Initialize()
        {
            // Set initial positions of map chunk views based on the positions of corresponding MapChunks.
            map.MapGrid.MapChunks.ForEach(chunk => testMapChunkView[chunk.Id].position = new Vector2(chunk.Position.X, chunk.Position.Y));

            // Subscribe to MapChunkMessage events to update the visual representation when a map chunk changes.
            messageManager.Subscribe<MapChunkMessage>(message =>
            {
                var mapChunk = message.Data;
                var chunkPosition = mapChunk.Position;

                // Update the position of the corresponding map chunk view.
                testMapChunkView[mapChunk.Id].position = new Vector2(chunkPosition.X, chunkPosition.Y);
            });
        }
    }
}