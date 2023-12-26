namespace Core.Contracts.Enemy
{
    /// <summary>
    /// Interface for an enemy spawner, responsible for spawning enemies in the game.
    /// </summary>
    public interface IEnemySpawner
    {
        /// <summary>
        /// Spawns an enemy in the game.
        /// </summary>
        void SpawnEnemy();
    }
}