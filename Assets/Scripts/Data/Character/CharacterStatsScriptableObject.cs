using UnityEngine;

namespace Data.Character
{
    /// <summary>
    /// ScriptableObject representing character stats.
    /// </summary>
    [CreateAssetMenu(menuName = "Data/CharacterStats")]
    public class CharacterStatsScriptableObject : ScriptableObject
    {
        /// <summary>
        /// Move speed of the character.
        /// </summary>
        public float moveSpeed;

        /// <summary>
        /// Acceleration of the character.
        /// </summary>
        public float acceleration;

        /// <summary>
        /// Deacceleration of the character.
        /// </summary>
        public float deAcceleration;
    }
}