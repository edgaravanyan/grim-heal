using System.Collections.Generic;
using Core.Character.CharacterStates;
using Core.StateMachine;

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
    }
}