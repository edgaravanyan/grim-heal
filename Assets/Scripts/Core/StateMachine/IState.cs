namespace Core.StateMachine
{
    /// <summary>
    /// Represents the contract for any state in the state machine.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called when entering the state.
        /// </summary>
        void Enter();

        /// <summary>
        /// Called to update the logical aspects of the state.
        /// </summary>
        void UpdateLogic();

        /// <summary>
        /// Called to update the physics-related aspects of the state.
        /// </summary>
        void UpdatePhysics();

        /// <summary>
        /// Handles input for the state.
        /// </summary>
        void HandleInput();

        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        void Exit();
    }
}