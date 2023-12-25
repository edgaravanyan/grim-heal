using System.Collections.Generic;
using Core.Map;

namespace Core.Contracts.Map
{
    /// <summary>
    /// Represents an interface for a map, composed of map chunks.
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Gets the list of map chunks that compose the map.
        /// </summary>
        List<MapChunk> Chunks { get; }
    }
}