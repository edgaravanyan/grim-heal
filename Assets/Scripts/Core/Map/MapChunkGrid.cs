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

        private List<MapChunk> chunkGrid;
        private MapChunk activeChunk;

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

        private void CreateGrid(int gridWidth, int gridHeight, Vector2 chunkSize)
        {
            chunkGrid = new List<MapChunk>();
            for (var row = 0; row < gridWidth; row++)
            {
                for (var col = 0; col < gridHeight; col++)
                {
                    var mapChunk = chunkPool.Get();
                    var position = new Vector2(col - 1, row - 1) * chunkSize;

                    var initialIndex = ToIndex(col, row);
                    mapChunk.Initialize(position, initialIndex);
                    mapChunk.OnPositionChanged += UpdateMap;

                    chunkGrid.Add(mapChunk);
                }
            }
            activeChunk = FindCurrentChunk(new Vector2(0, 0));
        }

        /// <summary>
        /// Handles position updates and updates the active map chunk accordingly.
        /// </summary>
        /// <param name="message">The position update message.</param>
        public void UpdateActiveChunkIfNeeded(PositionUpdateMessage message)
        {
            var position = message.Data;
            if (activeChunk.Contains(position)) return;

            // Update the active chunk based on the new position.
            activeChunk = FindCurrentChunk(position);
            ShiftChunksToCurrent();
        }

        private void ShiftChunksToCurrent()
        {
            // Shift chunks based on the new active chunk position.
            var direction = FromIndex(activeChunk.IndexInGrid) * -1;
            GridUtils.ShiftCells(chunkGrid, GridWidth, direction, wrappedCells);
            UpdateChunkIndicesAndPositions();
        }

        private void UpdateChunkIndicesAndPositions()
        {
            // Update indices and positions after shifting chunks.
            chunkGrid.ForEach(chunk => chunk.IndexInGrid = chunkGrid.IndexOf(chunk));
            wrappedCells.ForEach(chunk => chunk.Position = activeChunk.Position + FromIndex(chunk.IndexInGrid) * ChunkSize);
            wrappedCells.Clear();
        }

        private void UpdateMap(MapChunk chunk)
        {
            // Invoke the OnMapChunkUpdated event to notify subscribers of map chunk updates.
            OnMapChunkUpdated?.Invoke(chunk);
        }

        private MapChunk FindCurrentChunk(Vector2 position)
        {
            // Find the map chunk that contains a given position.
            return chunkGrid.First(chunk => chunk.Contains(position));
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
