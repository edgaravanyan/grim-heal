using System;
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
        private ICharacterStats characterStats;

        /// <summary>
        /// Event triggered when character statistics are updated.
        /// </summary>
        public event Action<ICharacterStats> OnStatsUpdated;

        /// <summary>
        /// Gets or sets the character statistics, such as move speed and acceleration.
        /// </summary>
        public ICharacterStats CharacterStats
        {
            set
            {
                OnStatsUpdated?.Invoke(value);
                characterStats = value;
            }
        }

        /// <summary>
        /// Gets or sets the position of the character in a 2D coordinate system.
        /// </summary>
        public IPosition Position { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="position">The initial position of the character.</param>
        public Character(IPosition position)
        {
            Position = position;
        }
    }
}