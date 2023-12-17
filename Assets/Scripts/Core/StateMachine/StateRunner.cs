using System;
using System.Collections.Generic;

namespace Core.StateMachine
{
    /// <summary>
    /// An abstract base class representing a generic state runner. 
    /// This class provides basic functionality for managing states within a state machine.
    /// </summary>
    /// <typeparam name="T">The type of states managed by the state runner.</typeparam>
    public abstract class StateRunner<T> : IStateRunner where T : IState
    {
        protected IState currentState;
        private readonly IEnumerable<T> states;

        /// <summary>
        /// Constructor for StateRunner.
        /// </summary>
        /// <param name="states">The collection of states managed by the state runner.</param>
        protected StateRunner(IEnumerable<T> states)
        {
            this.states = states;
        }

        /// <summary>
        /// Gets the state instance based on the provided state type.
        /// </summary>
        /// <param name="stateType">The type of the state to retrieve.</param>
        /// <returns>The instance of the state.</returns>
        private IState GetState(Type stateType)
        {
            foreach (var state in states)
            {
                if (state.GetType() == stateType)
                {
                    return state;
                }
            }

            throw new ArgumentException($"{GetType().Name} doesn't support the provided state: {stateType.Name}");
        }

        /// <summary>
        /// Sets the current state based on the provided state type.
        /// </summary>
        /// <param name="stateType">The type of the state to set.</param>
        public void SetState(Type stateType)
        {
            currentState?.Exit();
            currentState = GetState(stateType);
            currentState.Enter();
        }

        /// <summary>
        /// Updates the logic of the current state. 
        /// Concrete implementations should provide the specific update logic.
        /// </summary>
        public abstract void UpdateCurrentState();
    }
}
