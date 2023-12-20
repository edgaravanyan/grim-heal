using System;
using System.Collections.Generic;
using Application.Managers;
using Core.Character.CharacterStates;
using Core.Contracts;
using Core.Contracts.Messages;
using Core.MessagePipe.Messages;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.Character
{
    /// <summary>
    /// Controls character animations based on incoming messages.
    /// </summary>
    public class CharacterAnimationController : IInitializable
    {
        private static readonly int IdleTriggerHash = Animator.StringToHash("Idle");
        private static readonly int WalkTriggerHash = Animator.StringToHash("Walk");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");

        [Inject] private ICharacterView characterView;
        [Inject] private IMessageManager messageManager;

        private readonly Dictionary<Type, Action> messageHandlers = new ();

        /// <summary>
        /// Initializes the CharacterAnimationController.
        /// </summary>
        void IInitializable.Initialize()
        {
            RegisterMessageHandlers();
            RegisterSubscribers();
        }

        /// <summary>
        /// Plays the idle animation.
        /// </summary>
        public void PlayIdleAnimation()
        {
            characterView.PlayAnimation(IdleTriggerHash);
        }

        /// <summary>
        /// Plays the walk animation.
        /// </summary>
        public void PlayWalkAnimation()
        {
            characterView.PlayAnimation(WalkTriggerHash);
        }

        /// <summary>
        /// Plays the die animation.
        /// </summary>
        public void PlayDieAnimation()
        {
            characterView.PlayAnimation(DieTriggerHash);
        }

        private void RegisterSubscribers()
        {
            // Subscribe to animation messages.
            messageManager.Subscribe<CharacterAnimationMessage>(message =>
            {
                if (messageHandlers.TryGetValue(message.Data, out var handler))
                {
                    handler.Invoke();
                }
            });
        }

        private void RegisterMessageHandlers()
        {
            messageHandlers.Add(typeof(IdleState), PlayIdleAnimation);
            messageHandlers.Add(typeof(WalkState), PlayWalkAnimation);
            messageHandlers.Add(typeof(DieState), PlayDieAnimation);
            // Add more message handlers as needed.
        }
    }
}
