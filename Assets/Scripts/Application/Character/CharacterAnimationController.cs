using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Character.CharacterStates;
using Assets.Scripts.Core.MessagePipe.Messages;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.Character
{
    /// <summary>
    /// Controls character animations based on incoming messages.
    /// </summary>
    public class CharacterAnimationController : IInitializable, IDisposable
    {
        private static readonly int IdleTriggerHash = Animator.StringToHash("Idle");
        private static readonly int WalkTriggerHash = Animator.StringToHash("Walk");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");

        [Inject] private ICharacterView characterView;
        [Inject] private ISubscriber<CharacterAnimationMessage> messageSubscriber;

        private readonly Dictionary<Type, Action> messageHandlers = new ();
        private IDisposable messageBag;

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
            var disposableBagBuilder = DisposableBag.CreateBuilder();

            // Subscribe to animation messages.
            messageSubscriber.Subscribe(message =>
            {
                if (messageHandlers.TryGetValue(message.data, out var handler))
                {
                    handler.Invoke();
                }
            }).AddTo(disposableBagBuilder);

            messageBag = disposableBagBuilder.Build();
        }

        private void RegisterMessageHandlers()
        {
            messageHandlers.Add(typeof(IdleState), PlayIdleAnimation);
            messageHandlers.Add(typeof(WalkState), PlayWalkAnimation);
            messageHandlers.Add(typeof(DieState), PlayDieAnimation);
            // Add more message handlers as needed.
        }

        /// <summary>
        /// Disposes of resources used by the CharacterAnimationController.
        /// </summary>
        public void Dispose()
        {
            messageBag.Dispose();
        }
    }
}
