using Assets.Scripts.Core.Contracts;
using UnityEngine;

namespace Application.Utils
{
    /// <summary>
    /// Represents a 2D position in a Cartesian coordinate system.
    /// </summary>
    public class Position : IPosition
    {
        // Internal storage for the position using Unity's Vector2.
        private Vector2 position;

        /// <summary>
        /// Gets or sets the X-coordinate of the position.
        /// </summary>
        public float X
        {
            get => position.x;
            set => position.x = value;
        }

        /// <summary>
        /// Gets or sets the Y-coordinate of the position.
        /// </summary>
        public float Y
        {
            get => position.y;
            set => position.y = value;
        }
    }
}