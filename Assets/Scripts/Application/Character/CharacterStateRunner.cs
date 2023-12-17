using System.Collections.Generic;
using Core.Character.CharacterStates;
using Core.StateMachine;

namespace Application.Character
{
    /// <summary>
    /// Manages the states for a character.
    /// </summary>
    public class CharacterStateRunner : StateRunner<CharacterState>
    {
        /// <summary>
        /// Constructor for CharacterStateRunner.
        /// </summary>
        public CharacterStateRunner(IEnumerable<CharacterState> states) : base(states) { }

        /// <summary>
        /// Updates the logic of the current state, including input handling, logic update, and physics update.
        /// </summary>
        public override void UpdateCurrentState()
        {
            currentState.HandleInput();
            currentState.UpdateLogic();
            currentState.UpdatePhysics();
        }
    }
}