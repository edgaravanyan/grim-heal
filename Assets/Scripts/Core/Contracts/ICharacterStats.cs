namespace Assets.Scripts.Core.Contracts
{
    /// <summary>
    /// Represents the stats of a character.
    /// </summary>
    public interface ICharacterStats
    {
        /// <summary>
        /// Gets the move speed of the character.
        /// </summary>
        float MoveSpeed { get; }

        /// <summary>
        /// Gets the acceleration of the character.
        /// </summary>
        float Acceleration { get; }

        /// <summary>
        /// Gets the deceleration of the character.
        /// </summary>
        float DeAcceleration { get; }
    }
}