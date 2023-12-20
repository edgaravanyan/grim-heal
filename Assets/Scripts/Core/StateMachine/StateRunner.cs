using System;
using System.Collections.Generic;
using System.Numerics;

namespace Core.StateMachine
{
    /// <summary>
    /// An abstract base class representing a generic state runner.
    /// This class provides basic functionality for managing states within a state machine.
    /// </summary>
    /// <typeparam name="T">The type of states managed by the state runner.</typeparam>
    public abstract class StateRunner<T> : IStateRunner where T : IState
    {
        private readonly IEnumerable<T> states;

        /// <summary>
        /// The current state managed by the state runner.
        /// </summary>
        protected IState currentState;

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

            throw new ArgumentException($"{GetType().Name} doesn't support the provided state type: {stateType.Name}");
        }

        /// <summary>
        /// Sets the current state based on the provided state type.
        /// </summary>
        /// <param name="stateType">The type of the state to set.</param>
        public void SetState(Type stateType)
        {
            if (currentState == null || currentState.GetType() != stateType)
            {
                currentState?.Exit();
                currentState = GetState(stateType);
                currentState.Enter();
            }
        }

        /// <summary>
        /// Sets the current state to the specified type.
        /// </summary>
        /// <typeparam name="TState">The type of the state to set.</typeparam>
        public void SetState<TState>() where TState : IState
        {
            SetState(typeof(TState));
        }

        /// <summary>
        /// Handles input for the current state.
        /// </summary>
        /// <param name="input">The input vector representing user input.</param>
        public void HandleInput(Vector2 input)
        {
            foreach (var state in states)
            {
                state.HandleInput(input);
            }
        }

        /// <summary>
        /// Updates the logic of the current state.
        /// Concrete implementations should provide the specific update logic.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public virtual void UpdateCurrentState(float deltaTime)
        {
            currentState.UpdateLogic(deltaTime);
            currentState.CheckToChange();
        }

        /// <summary>
        /// Handles physics-related updates for the current state.
        /// Concrete implementations should provide the specific fixed update logic.
        /// </summary>
        /// <param name="fixedDeltaTime">The fixed time step between frames.</param>
        public virtual void FixedUpdate(float fixedDeltaTime)
        {
            currentState.UpdatePhysics(fixedDeltaTime);
        }
    }
}
