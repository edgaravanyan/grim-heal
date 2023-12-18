using Assets.Scripts.Core.Contracts;

namespace Assets.Scripts.Core.Character
{
    /// <summary>
    /// A model class representing a game character.
    /// </summary>
    /// <remarks>
    /// Contains essential character-related data and basic behavior.
    /// </remarks>
    public class Character
    {
        /// <summary>
        /// Gets or sets the position of the character in a 2D coordinate system.
        /// </summary>
        public IPosition Position { get; }

        public Character(IPosition position)
        {
            Position = position;
        }
    }
}