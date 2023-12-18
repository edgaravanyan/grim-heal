using System.Numerics;

namespace Assets.Scripts.Core.Contracts
{
    /// <summary>
    /// Represents a 2D position in a Cartesian coordinate system.
    /// </summary>
    public interface IPosition
    {
        /// <summary>
        /// Gets or sets the X-coordinate of the position.
        /// </summary>
        float X { get; set; }

        /// <summary>
        /// Gets or sets the Y-coordinate of the position.
        /// </summary>
        float Y { get; set; }

        /// <summary>
        /// Calculates the squared length of the position vector.
        /// </summary>
        /// <returns>The squared length of the position vector.</returns>
        float SquaredLength();

        /// <summary>
        /// Sets the position based on a Vector2.
        /// </summary>
        /// <param name="position">The Vector2 representing the new position.</param>
        void Set(Vector2 position);
    }
}