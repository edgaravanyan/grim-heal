using Core.Contracts;
using UnityEngine;

namespace Application.Utils
{
    /// <summary>
    /// Represents a 2D position in a Cartesian coordinate system using Unity's Vector2.
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

        /// <summary>
        /// Gets the squared length of the position vector.
        /// </summary>
        /// <returns>The squared length of the position vector.</returns>
        public float SquaredLength()
        {
            return position.sqrMagnitude;
        }

        /// <summary>
        /// Sets the position from System.Numerics.Vector2.
        /// </summary>
        /// <param name="position">The System.Numerics.Vector2 representing the new position.</param>
        public void Set(System.Numerics.Vector2 position)
        {
            this.position = new Vector2(position.X, position.Y);
        }

        public override string ToString()
        {
            return this.position.ToString();
        }
    }
}