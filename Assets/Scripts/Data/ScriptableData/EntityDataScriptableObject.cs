using UnityEngine;

namespace Data.ScriptableData
{
    /// <summary>
    /// ScriptableObject containing data for an entity, such as health and move speed.
    /// </summary>
    [CreateAssetMenu(menuName = "Data/CharacterData")]
    public class EntityDataScriptableObject : ScriptableObject
    {
        /// <summary>
        /// Health of the entity.
        /// </summary>
        public int health;

        /// <summary>
        /// Move speed of the entity.
        /// </summary>
        public float moveSpeed;
    }
}