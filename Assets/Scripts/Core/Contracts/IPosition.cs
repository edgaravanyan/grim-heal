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
    }
}