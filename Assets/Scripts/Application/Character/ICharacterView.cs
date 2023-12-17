namespace Application.Character
{
    /// <summary>
    /// Interface for character view.
    /// </summary>
    public interface ICharacterView
    {
        /// <summary>
        /// Plays the animation based on the provided trigger.
        /// </summary>
        /// <param name="trigger">The trigger for the animation.</param>
        void PlayAnimation(int trigger);
    }
}