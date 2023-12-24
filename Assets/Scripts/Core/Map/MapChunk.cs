using System;
using System.Numerics;
using Core.Contracts.Pool;

namespace Core.Map
{
    /// <summary>
    /// Represents a chunk in a map, implementing the IPoolable interface.
    /// </summary>
    public class MapChunk : IPoolable
    {
        private Vector2 position;

        /// <summary>
        /// Event triggered when the position of the MapChunk changes.
        /// </summary>
        public event Action<MapChunk> OnPositionChanged;

        /// <summary>
        /// Gets the unique identifier of the MapChunk.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the index of the MapChunk in the grid.
        /// </summary>
        public int IndexInGrid { get; set; }

        /// <summary>
        /// Gets or sets the position of the MapChunk.
        /// Triggers the OnPositionChanged event when the position is set.
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                OnPositionChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Initializes the MapChunk with the specified position and index.
        /// </summary>
        /// <param name="position">The position of the MapChunk.</param>
        /// <param name="index">The index of the MapChunk.</param>
        public void Initialize(Vector2 position, int index)
        {
            this.Id = index;
            this.Position = position;
            this.IndexInGrid = index;
        }

        /// <summary>
        /// Checks if the given position is within the boundaries of the MapChunk.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>True if the position is within the boundaries; otherwise, false.</returns>
        public bool Contains(Vector2 position)
        {
            var relativePosition = position - this.Position;
            var chunkHalfSize = MapChunkGrid.ChunkSize * 0.5f;
            return relativePosition.X <= chunkHalfSize.X &&
                   relativePosition.Y <= chunkHalfSize.Y &&
                   relativePosition.X >= -chunkHalfSize.X &&
                   relativePosition.Y >= -chunkHalfSize.Y;
        }
    }
}
