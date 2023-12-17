using System;
using Application.Character;
using Core.Character;
using Core.Character.CharacterStates;
using Core.MessagePipe;
using Core.MessagePipe.Messages;
using Core.StateMachine;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.Character;
using CharacterController = Application.Character.CharacterController;

namespace IoC
{
    /// <summary>
    /// Configures the lifetime scope for VContainer in the game, providing a centralized place for dependency registration.
    /// </summary>
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private CharacterView characterView;

        /// <summary>
        /// Configures the dependencies and their lifetimes for the game.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMessagePipe(builder);
            RegisterCharacter(builder);
        }

        /// <summary>
        /// Registers the MessagePipe and its dependencies.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterMessagePipe(IContainerBuilder builder)
        {
            // Register MessagePipe and set up options.
            var options = builder.RegisterMessagePipe();

            // Setup GlobalMessagePipe to enable diagnostics window and global function.
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

            // Register MessageBroker for CharacterAnimationMessage messages.
            builder.RegisterMessageBroker<CharacterAnimationMessage>(options);
            builder.Register<PoolableMessagePublisher<CharacterAnimationMessage>>(Lifetime.Singleton);
        }

        /// <summary>
        /// Registers character-related dependencies.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterCharacter(IContainerBuilder builder)
        {
            builder.Register<Character>(Lifetime.Scoped);
            builder.Register<CharacterState, IdleState>(Lifetime.Scoped);
            builder.Register<CharacterState, WalkState>(Lifetime.Scoped);
            builder.Register<CharacterState, DieState>(Lifetime.Scoped);

            // Register the CharacterView component.
            builder.RegisterComponent(characterView).As<ICharacterView>();
            // Register CharacterAnimationController as an entry point.
            builder.Register<CharacterAnimationController>(Lifetime.Singleton).AsSelf();
            // Register CharacterStateRunner as a StateRunner<CharacterState>.
            builder.Register<CharacterStateRunner>(Lifetime.Scoped).As<StateRunner<CharacterState>>();
            // Register CharacterController as a self-contained dependency.
            builder.RegisterEntryPoint<CharacterController>(Lifetime.Scoped).AsSelf();
        }
    }
}
