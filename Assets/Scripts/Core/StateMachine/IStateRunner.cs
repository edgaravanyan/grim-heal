using System;
using System.Numerics;

namespace Assets.Scripts.Core.StateMachine
{
    /// <summary>
    /// Interface for a state machine runner.
    /// </summary>
    public interface IStateRunner
    {
        /// <summary>
        /// Sets the current state to the specified type.
        /// </summary>
        /// <param name="stateType">The type of the new state.</param>
        void SetState(Type stateType);

        /// <summary>
        /// Updates the logic of the current state.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        void UpdateCurrentState(float deltaTime);

        /// <summary>
        /// Handles physics-related updates for the current state.
        /// </summary>
        /// <param name="fixedDeltaTime">The fixed time step between frames.</param>
        void FixedUpdate(float fixedDeltaTime);

        /// <summary>
        /// Handles input for the current state.
        /// </summary>
        /// <param name="input">The input vector representing user input.</param>
        void HandleInput(Vector2 input);
    }
}