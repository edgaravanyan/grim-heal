using System.Numerics;

namespace Core.Contracts.Character
{
    /// <summary>
    /// Interface for character view.
    /// </summary>
    public interface IAnimatedView
    {
        /// <summary>
        /// Plays the animation based on the provided trigger.
        /// </summary>
        /// <param name="trigger">The trigger for the animation.</param>
        void PlayAnimation(int trigger);

        /// <summary>
        /// Sets the position of the character view.
        /// </summary>
        /// <param name="position">The new position.</param>
        void SetPosition(Vector2 position);
    }
}