using System.Collections.Generic;
using Core.Contracts.Map;
using Core.Contracts.Messages;
using Core.Contracts.Pool;

namespace Core.Map
{
    /// <summary>
    /// Represents a 2D map divided into chunks.
    /// </summary>
    public class Map : IMap
    {
        private readonly MapMessageHandler messageHandler;
        private readonly MapChunkGrid mapGrid;

        /// <summary>
        /// Gets the list of map chunks.
        /// </summary>
        public List<MapChunk> Chunks => mapGrid.MapChunks;

        /// <summary>
        /// Initializes a new instance of the Map class.
        /// </summary>
        /// <param name="messageManager">The message manager for handling inter-object communication.</param>
        /// <param name="chunkPool">The object pool for map chunks.</param>
        public Map(IMessageManager messageManager, IObjectPool<MapChunk> chunkPool)
        {
            // Create an instance of MapMessageHandler to handle message subscriptions and updates.
            messageHandler = new MapMessageHandler(messageManager);

            // Create an instance of MapChunkGrid to manage the grid of map chunks.
            mapGrid = new MapChunkGrid(chunkPool);

            // Subscribe the MapChunkGrid to the OnMapChunkUpdated event to receive notifications of chunk updates.
            mapGrid.OnMapChunkUpdated += messageHandler.PublishMapChunkUpdate;

            // Subscribe the MapMessageHandler to position updates to handle changes in the active chunk.
            messageHandler.SubscribeToPositionUpdates(mapGrid.UpdateActiveChunkIfNeeded);
        }
    }
}