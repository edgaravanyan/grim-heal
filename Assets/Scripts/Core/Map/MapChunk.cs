using System;
using System.Numerics;
using Core.Contracts.Pool;

namespace Core.Map
{
    public class MapChunk : IPoolable
    {
        private Vector2 position;
        
        public event Action<MapChunk> OnPositionChanged;

        public int Id { get; private set; }
        public int IndexInGrid { get; set; }
        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                OnPositionChanged?.Invoke(this);
            }
        }

        public void Initialize(Vector2 position, int index)
        {
            this.Id = index;
            this.Position = position;
            this.IndexInGrid = index;
        }

        public bool Contains(Vector2 position)
        {
            var relativePosition = position - this.Position;
            var chunkHalfSize = MapChunkGrid.ChunkSize * 0.5f;
            return relativePosition.X <=  chunkHalfSize.X && 
                   relativePosition.Y <=  chunkHalfSize.Y &&
                   relativePosition.X >= -chunkHalfSize.X &&
                   relativePosition.Y >= -chunkHalfSize.Y;
        }
    }
}
