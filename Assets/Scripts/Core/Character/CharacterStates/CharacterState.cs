using System;
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
        [Inject] protected Character character;
        [Inject] protected PoolableMessagePublisher<CharacterAnimationMessage, Type> animationPublisher;

        /// <summary>
        /// Called when entering the state.
        /// </summary>
        public virtual void Enter() { }

        /// <summary>
        /// Called to update logic of the state.
        /// </summary>
        public virtual void UpdateLogic() { }

        /// <summary>
        /// Called to update physics of the state.
        /// </summary>
        public virtual void UpdatePhysics() { }

        /// <summary>
        /// Handles input for the state.
        /// </summary>
        public virtual void HandleInput() { }

        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        public virtual void Exit() { }
    }
}