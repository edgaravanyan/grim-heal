using Core.Contracts;

namespace Core.Enemy
{
    /// <summary>
    /// Represents an enemy entity in the game.
    /// </summary>
    public class Enemy
    {
        private IEntityData enemyData;
        private EnemyType enemyType;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="enemyData">The data associated with the enemy.</param>
        /// <param name="enemyType">The type of the enemy.</param>
        public Enemy(IEntityData enemyData, EnemyType enemyType)
        {
            this.enemyData = enemyData;
            this.enemyType = enemyType;
        }
    }
}