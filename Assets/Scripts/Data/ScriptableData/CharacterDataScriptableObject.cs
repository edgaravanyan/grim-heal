using UnityEngine;

namespace Data.ScriptableData
{
    /// <summary>
    /// ScriptableObject representing character data.
    /// </summary>
    [CreateAssetMenu(menuName = "Data/CharacterData")]
    public class CharacterDataScriptableObject : EntityDataScriptableObject
    {
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