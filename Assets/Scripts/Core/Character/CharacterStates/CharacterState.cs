using System.Numerics;
using Core.Contracts.Messages;
using Core.MessagePipe.Messages;
using Core.StateMachine;

namespace Core.Character.CharacterStates
{
    /// <summary>
    /// An abstract adapter of IState inherited by specific states of the character.
    /// </summary>
    /// <remarks>
    /// Inherit this class in concrete state classes to implement character state behavior.
    /// </remarks>
    public abstract class CharacterState : IState
    {
        protected Vector2 Input;
        protected Character character;
        protected IMessageManager messageManager;

        public CharacterState(Character character, IMessageManager messageManager)
        {
            this.character = character;
            this.messageManager = messageManager;
        }

        /// <summary>
        /// Called when entering the state. Publishes an animation message for the current state type.
        /// </summary>
        public virtual void Enter()
        {
            messageManager.Publish<CharacterAnimationMessage>(this.GetType());
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
        /// Checks conditions to determine whether to change to a different state.
        /// </summary>
        public virtual void CheckToChange() { }

        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        public virtual void Exit() { }
    }
}
