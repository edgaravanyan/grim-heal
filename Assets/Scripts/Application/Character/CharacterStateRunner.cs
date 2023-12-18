using System.Collections.Generic;
using System.Numerics;
using Assets.Scripts.Core.Character.CharacterStates;
using Assets.Scripts.Core.StateMachine;

namespace Application.Character
{
    /// <summary>
    /// Manages the states for a character, facilitating the transition between different character states.
    /// </summary>
    public class CharacterStateRunner : StateRunner<CharacterState>
    {
        /// <summary>
        /// Constructor for CharacterStateRunner.
        /// </summary>
        /// <param name="states">Initial collection of character states.</param>
        public CharacterStateRunner(IEnumerable<CharacterState> states) : base(states) { }

        /// <summary>
        /// Updates the logical aspects of the current state, including handling input, logic updates, and physics updates.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public override void UpdateCurrentState(float deltaTime)
        {
            // Update the logical aspects of the current state.
            currentState.UpdateLogic(deltaTime);
        }

        /// <summary>
        /// Called at a fixed time step for physics-related updates in the current state.
        /// </summary>
        /// <param name="fixedDeltaTime">The fixed time step for physics calculations.</param>
        public override void FixedUpdate(float fixedDeltaTime)
        {
            // Update the physics-related aspects of the current state.
            currentState.UpdatePhysics(fixedDeltaTime);
        }

        /// <summary>
        /// Handles input for the current state.
        /// </summary>
        /// <param name="input">The input vector representing user input.</param>
        public override void HandleInput(Vector2 input)
        {
            // Pass the input to the current state for handling.
            currentState.HandleInput(input);
        }
    }
}