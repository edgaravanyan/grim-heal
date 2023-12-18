using System;
using Application.Character;
using Application.Input;
using Application.Messages;
using Application.Utils;
using Assets.Scripts.Core.Character;
using Assets.Scripts.Core.Character.CharacterStates;
using Assets.Scripts.Core.Contracts;
using Assets.Scripts.Core.Contracts.Pool;
using Assets.Scripts.Core.Logger;
using Assets.Scripts.Core.MessagePipe;
using Assets.Scripts.Core.MessagePipe.Messages;
using Assets.Scripts.Core.StateMachine;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.Character;
using CharacterController = Application.Character.CharacterController;
using Logger = Application.Utils.Logger;

namespace IoC
{
    /// <summary>
    /// Configures the lifetime scope for VContainer in the game.
    /// Provides a centralized place for dependency registration.
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
            RegisterInput(builder);
            RegisterMessagePipe(builder);
            RegisterCharacter(builder);
            RegisterCoreAdapters(builder);
        }

        private void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        /// <summary>
        /// Registers adapters for core utilities.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterCoreAdapters(IContainerBuilder builder)
        {
            builder.Register<IMessagePublisher<CharacterAnimationMessage>, MessagePublisher<CharacterAnimationMessage>>(Lifetime.Singleton);
            builder.Register<IMessagePublisher<PositionUpdateMessage>, MessagePublisher<PositionUpdateMessage>>(Lifetime.Singleton);
            builder.Register<IMessagePublisher<SetCharacterStateMessage>, MessagePublisher<SetCharacterStateMessage>>(Lifetime.Singleton);
            
            builder.Register<IObjectPool<CharacterAnimationMessage>, ObjectPoolAdapter<CharacterAnimationMessage>>(Lifetime.Singleton);
            builder.Register<IObjectPool<PositionUpdateMessage>, ObjectPoolAdapter<PositionUpdateMessage>>(Lifetime.Singleton);
            builder.Register<IObjectPool<SetCharacterStateMessage>, ObjectPoolAdapter<SetCharacterStateMessage>>(Lifetime.Singleton);
            
            builder.Register<IPosition, Position>(Lifetime.Transient);
            
            LoggerProvider.Initialize(new Logger());
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
            builder.RegisterMessageBroker<PositionUpdateMessage>(options);
            builder.RegisterMessageBroker<SetCharacterStateMessage>(options);
            
            builder.Register<PoolableMessagePublisher<CharacterAnimationMessage, Type>>(Lifetime.Singleton).AsSelf();
            builder.Register<PoolableMessagePublisher<PositionUpdateMessage, IPosition>>(Lifetime.Singleton).AsSelf();
            builder.Register<PoolableMessagePublisher<SetCharacterStateMessage, Type>>(Lifetime.Singleton).AsSelf();
        }

        /// <summary>
        /// Registers character-related dependencies.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterCharacter(IContainerBuilder builder)
        {
            builder.Register<Character>(Lifetime.Scoped).AsSelf();
            builder.Register<CharacterMovement>(Lifetime.Scoped).AsSelf();
            
            builder.Register<CharacterState, IdleState>(Lifetime.Scoped);
            builder.Register<CharacterState, WalkState>(Lifetime.Scoped);
            builder.Register<CharacterState, DieState>(Lifetime.Scoped);

            // Register the CharacterView component.
            builder.RegisterComponent(characterView).As<ICharacterView>();

            // Register the CharacterMovementController component.
            builder.Register<CharacterMovementController>(Lifetime.Scoped).AsImplementedInterfaces();
            
            // Register CharacterAnimationController component.
            builder.Register<CharacterAnimationController>(Lifetime.Scoped).AsImplementedInterfaces();
            
            // Register CharacterStateRunner as a StateRunner<CharacterState>.
            builder.Register<CharacterStateRunner>(Lifetime.Singleton).As<StateRunner<CharacterState>>();
            
            // Register CharacterController as a self-contained entry point.
            builder.RegisterEntryPoint<CharacterController>(Lifetime.Scoped).AsSelf();
        }
    }
}
