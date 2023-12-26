namespace Data.ScriptableData
{
    /// <summary>
    /// Represents data for an entity, such as health and move speed, loaded from a ScriptableObject.
    /// </summary>
    /// <typeparam name="TData">The type of the ScriptableObject containing the entity data.</typeparam>
    public class EntityData<TData> : Data<TData> where TData : EntityDataScriptableObject
    {
        /// <summary>
        /// Gets the health of the entity.
        /// </summary>
        public float Health => scriptableObjectData.health;

        /// <summary>
        /// Gets the move speed of the entity.
        /// </summary>
        public float MoveSpeed => scriptableObjectData.moveSpeed;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityData{TData}"/> class.
        /// </summary>
        /// <param name="data">The ScriptableObject containing the entity data.</param>
        public EntityData(TData data) : base(data) { }
    }
}