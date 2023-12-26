namespace Core.Contracts.Character
{
    /// <summary>
    /// Represents the data of a character.
    /// </summary>
    public interface ICharacterData : IEntityData
    {
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