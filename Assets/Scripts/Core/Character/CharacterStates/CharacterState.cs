using System;
using System.Numerics;
using Assets.Scripts.Core.MessagePipe;
using Assets.Scripts.Core.MessagePipe.Messages;
using Assets.Scripts.Core.StateMachine;
using VContainer;

namespace Assets.Scripts.Core.Character.CharacterStates
{
    /// <summary>
    /// An abstract adapter of IState inherited by specific states of the character.
    /// </summary>
    /// <remarks>
    /// Inherit this class in concrete state classes to implement character state behavior.
    /// </remarks>
    public abstract class CharacterState : IState
    {
        /// <summary>
        /// Gets or sets the input vector representing user input for the state.
        /// </summary>
        protected Vector2 Input;

        [Inject] protected PoolableMessagePublisher<CharacterAnimationMessage, Type> animationPublisher;

        /// <summary>
        /// Called when entering the state. Publishes an animation message for the current state type.
        /// </summary>
        public virtual void Enter()
        {
            animationPublisher.Publish(this.GetType());
        }

        /// <summary>
        /// Called to update the logical aspects of the state.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public virtual void UpdateLogic(float deltaTime) { }

        /// <summary>
        /// Called to update the physics-related aspects of the state.
        /// </summary>
        /// <param name="fixedDeltaTime">The fixed time step for physics calculations.</param>
        public virtual void UpdatePhysics(float fixedDeltaTime) { }

        /// <summary>
        /// Handles input for the state, updating the Input property.
        /// </summary>
        /// <param name="input">The input vector representing user input.</param>
        public virtual void HandleInput(Vector2 input)
        {
            Input = input;
        }

        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        public virtual void Exit() { }
    }
}
