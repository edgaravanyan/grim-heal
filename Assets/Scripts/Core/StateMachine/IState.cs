using System.Numerics;

namespace Core.StateMachine
{
    /// <summary>
    /// Represents the contract for any state in the state machine.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called when entering the state. Implement this method to initialize any necessary resources or perform setup actions.
        /// </summary>
        void Enter();

        /// <summary>
        /// Called to update the logical aspects of the state. Implement this method to handle game logic updates.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        void UpdateLogic(float deltaTime);

        /// <summary>
        /// Called to update the physics-related aspects of the state. Implement this method to handle physics-related updates.
        /// </summary>
        /// <param name="fixedDeltaTime">The fixed time step for physics calculations.</param>
        void UpdatePhysics(float fixedDeltaTime);

        /// <summary>
        /// Handles input for the state. Implement this method to process user input specific to the state.
        /// </summary>
        /// <param name="input">The input vector representing user input for movement.</param>
        void HandleInput(Vector2 input);

        /// <summary>
        /// Checks conditions to determine whether to change to a different state.
        /// </summary>
        void CheckToChange();

        /// <summary>
        /// Called when exiting the state. Implement this method to clean up resources or perform exit actions.
        /// </summary>
        void Exit();
    }
}