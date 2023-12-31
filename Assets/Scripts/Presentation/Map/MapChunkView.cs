using Core.Contracts.Map;
using UnityEngine;

namespace Presentation.Map
{
    /// <summary>
    /// Unity MonoBehaviour representing a view for a map.
    /// </summary>
    public class MapChunkView : MonoBehaviour, IMapView
    {
        /// <summary>
        /// Sets the position of the map view.
        /// </summary>
        /// <param name="position">The position to set for the map view.</param>
        public void SetPosition(System.Numerics.Vector2 position)
        {
            transform.position = new Vector3(position.X, position.Y);
        }
    }
}