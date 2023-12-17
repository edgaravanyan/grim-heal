using System;
using System.Collections.Generic;
using Core.MessagePipe.Messages;
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
        public static readonly int IdleTriggerHash = Animator.StringToHash("Idle");
        public static readonly int DieTriggerHash = Animator.StringToHash("Die");

        [Inject] private ICharacterView characterView;
        [Inject] private ISubscriber<CharacterAnimationMessage> messageSubscriber;

        private readonly Dictionary<int, Action> messageHandlers = new Dictionary<int, Action>();
        private IDisposable messageBag;

        /// <summary>
        /// Initializes the CharacterAnimationController.
        /// </summary>
        public void Initialize()
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
                if (messageHandlers.TryGetValue(message.AnimationHash, out var handler))
                {
                    handler.Invoke();
                }
            }).AddTo(disposableBagBuilder);

            messageBag = disposableBagBuilder.Build();
        }

        private void RegisterMessageHandlers()
        {
            messageHandlers.Add(IdleTriggerHash, PlayIdleAnimation);
            messageHandlers.Add(DieTriggerHash, PlayDieAnimation);
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
