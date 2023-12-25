using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Core.Contracts.Pool;
using Core.MessagePipe.Messages;
using Core.Utils;

namespace Core.Map
{
    /// <summary>
    /// Manages the grid of map chunks.
    /// </summary>
    public class MapChunkGrid
    {
        private const int GridWidth = 3;
        private const int GridHeight = 3;

        private readonly IObjectPool<MapChunk> chunkPool;
        private readonly List<MapChunk> wrappedCells = new(GridWidth * GridHeight);

        private MapChunk activeChunk;

        /// <summary>
        /// Gets the list of map chunks in the grid.
        /// </summary>
        public List<MapChunk> MapChunks { get; private set; }

        /// <summary>
        /// Gets the size of each map chunk.
        /// </summary>
        public static readonly Vector2 ChunkSize = new(20, 20);

        /// <summary>
        /// Event triggered when a map chunk is updated.
        /// </summary>
        public event Action<MapChunk> OnMapChunkUpdated;

        /// <summary>
        /// Initializes a new instance of the MapChunkGrid class.
        /// </summary>
        /// <param name="chunkPool">The object pool for managing map chunks.</param>
        public MapChunkGrid(IObjectPool<MapChunk> chunkPool)
        {
            this.chunkPool = chunkPool;

            // Create the initial grid of map chunks.
            CreateGrid(GridWidth, GridHeight, ChunkSize);
        }

        /// <summary>
        /// Check to update the active map chunk by character position.
        /// </summary>
        /// <param name="message">The position update message.</param>
        public void UpdateActiveChunkIfNeeded(PositionUpdateMessage message)
        {
            var position = message.Data;
            if (activeChunk.Contains(position)) return;

            activeChunk = FindCurrentChunk(position);
            ShiftChunksToCurrent();
        }

        private void CreateGrid(int gridWidth, int gridHeight, Vector2 chunkSize)
        {
            MapChunks = new List<MapChunk>();
            for (var row = 0; row < gridWidth; row++)
            {
                for (var col = 0; col < gridHeight; col++)
                {
                    var mapChunk = chunkPool.Get();
                    var position = new Vector2(col - 1, row - 1) * chunkSize;

                    var initialIndex = ToIndex(col, row);
                    mapChunk.Initialize(position, initialIndex);
                    mapChunk.OnPositionChanged += UpdateMap;

                    MapChunks.Add(mapChunk);
                }
            }
            activeChunk = FindCurrentChunk(new Vector2(0, 0));
        }

        private void ShiftChunksToCurrent()
        {
            // Shift chunks to keep active chunk in the middle by surrounding it
            var direction = FromIndex(activeChunk.IndexInGrid) * -1;
            GridUtils.ShiftCells(MapChunks, GridWidth, direction, wrappedCells);
            UpdateChunkIndicesAndPositions();
        }

        private void UpdateChunkIndicesAndPositions()
        {
            // Update indices and positions after shifting chunks.
            MapChunks.ForEach(chunk => chunk.IndexInGrid = MapChunks.IndexOf(chunk));
            wrappedCells.ForEach(chunk => chunk.Position = activeChunk.Position + FromIndex(chunk.IndexInGrid) * ChunkSize);
            wrappedCells.Clear();
        }

        private void UpdateMap(MapChunk chunk)
        {
            OnMapChunkUpdated?.Invoke(chunk);
        }

        private MapChunk FindCurrentChunk(Vector2 position)
        {
            return MapChunks.First(chunk => chunk.Contains(position));
        }

        private static int ToIndex(int x, int y)
        {
            // Convert 2D coordinates to a 1D index.
            return y * GridWidth + x;
        }

        private static Vector2 FromIndex(int index)
        {
            // Convert a 1D index to 2D coordinates.
            return new Vector2(index % GridWidth - 1, index / GridWidth - 1);
        }
    }
}
