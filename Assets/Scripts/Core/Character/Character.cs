using System;
using System.Numerics;
using Core.Contracts;

namespace Core.Character
{
    /// <summary>
    /// A model class representing a game character.
    /// </summary>
    /// <remarks>
    /// Contains essential character-related data and basic behavior.
    /// </remarks>
    public class Character
    {
        private ICharacterData characterData;

        /// <summary>
        /// Event triggered when character statistics are updated.
        /// </summary>
        public event Action<ICharacterData> OnStatsUpdated;

        /// <summary>
        /// Gets or sets the character statistics, such as move speed and acceleration.
        /// </summary>
        public ICharacterData CharacterData
        {
            set
            {
                OnStatsUpdated?.Invoke(value);
                characterData = value;
            }
        }

        /// <summary>
        /// Gets or sets the position of the character in a 2D coordinate system.
        /// </summary>
        public Vector2 Position { get; set; }
    }
}