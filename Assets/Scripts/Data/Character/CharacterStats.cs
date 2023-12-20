using Core.Contracts;

namespace Data.Character
{
    /// <summary>
    /// Implementation of the ICharacterStats interface using data from a CharacterStatsScriptableObject.
    /// </summary>
    public class CharacterStats : Data<CharacterStatsScriptableObject>, ICharacterStats
    {

        public CharacterStats(CharacterStatsScriptableObject stats) : base(stats) { }

        /// <summary>
        /// Gets the move speed of the character.
        /// </summary>
        public float MoveSpeed => scriptableObjectData.moveSpeed;

        /// <summary>
        /// Gets the acceleration of the character.
        /// </summary>
        public float Acceleration => scriptableObjectData.acceleration;

        /// <summary>
        /// Gets the deacceleration of the character.
        /// </summary>
        public float DeAcceleration => scriptableObjectData.deAcceleration;
    }
}