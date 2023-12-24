using UnityEngine;

namespace Data.Character
{
    /// <summary>
    /// ScriptableObject representing character data.
    /// </summary>
    [CreateAssetMenu(menuName = "Data/CharacterData")]
    public class CharacterDataScriptableObject : ScriptableObject
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