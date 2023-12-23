using System.Collections.Generic;
using Core.Contracts.Messages;
using Core.Map;
using Core.MessagePipe.Messages;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Presentation.Managers
{
    public class MapManager : MonoBehaviour, IInitializable
    {
        public List<Transform> testMapChunkView;

        [Inject] private Map map;
        [Inject] private IMessageManager messageManager;

        void IInitializable.Initialize()
        {
            // map.ChunkGrid.ForEach(chunk => testMapChunkView[chunk.Id].position = new Vector2(chunk.Position.X, chunk.Position.Y));
            messageManager.Subscribe<MapChunkMessage>(message =>
            {
                var mapChunk = message.Data;
                var chunkIndex = mapChunk.IndexInGrid;
                var chunkPosition = mapChunk.Position;
                
                testMapChunkView[mapChunk.Id].position = new Vector2(chunkPosition.X, chunkPosition.Y);
            });
        }
    }
}
