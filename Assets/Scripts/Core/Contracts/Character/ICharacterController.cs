namespace Core.Contracts.Character
{
    /// <summary>
    /// Controls and updates the character.
    /// </summary>
    public interface ICharacterController
    {
        /// <summary>
        /// Handles the character state update during each frame.
        /// </summary>
        void Tick();
        
        /// <summary>
        /// Handles the character update during fixed time steps for physics.
        /// </summary>
        void FixedTick();
    }
}
