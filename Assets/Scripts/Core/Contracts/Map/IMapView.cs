using System.Numerics;

namespace Core.Contracts.Map
{
    /// <summary>
    /// Represents an interface for a view associated with a map.
    /// </summary>
    public interface IMapView
    {
        /// <summary>
        /// Sets the position of the map view.
        /// </summary>
        /// <param name="position">The new position to set for the map view.</param>
        void SetPosition(Vector2 position);
    }
}