namespace Core.Contracts
{
    /// <summary>
    /// Represents the data of a character.
    /// </summary>
    public interface ICharacterData
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