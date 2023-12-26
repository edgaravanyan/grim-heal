namespace Core.Contracts
{
    /// <summary>
    /// Interface for entity data, representing common properties such as health and move speed.
    /// </summary>
    public interface IEntityData
    {
        /// <summary>
        /// Gets the health of the entity.
        /// </summary>
        float Health { get; }

        /// <summary>
        /// Gets the move speed of the entity.
        /// </summary>
        float MoveSpeed { get; }
    }
}