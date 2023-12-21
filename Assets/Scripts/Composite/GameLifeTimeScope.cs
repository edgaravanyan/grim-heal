using System;
using System.Collections.Generic;
using Application.Character;
using Application.Input;
using Application.Managers;
using Application.Messages;
using Application.Utils;
using Core.Character;
using Core.Character.CharacterStates;
using Core.Contracts;
using Core.Contracts.Messages;
using Core.Contracts.Pool;
using Core.Logger;
using Core.MessagePipe;
using Core.MessagePipe.Messages;
using Core.StateMachine;
using Data;
using Data.Character;
using MessagePipe;
using Presentation.Character;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using CharacterController = Application.Character.CharacterController;
using Logger = Application.Utils.Logger;

namespace Composite
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
            RegisterMessagePipe(builder);
            RegisterDataAdapters(builder);
            
            RegisterViewAdapters(builder);
            RegisterApplicationAdapters(builder);

            RegisterInput(builder);

            RegisterCharacter(builder);
        }
        
        /// <summary>
        /// Registers input dependencies
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        /// <summary>
        /// Registers adapters for core contracts in the view layer.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterViewAdapters(IContainerBuilder builder)
        {
            // Register the CharacterView component.
            builder.RegisterComponent(characterView).As<ICharacterView>();
        }

        /// <summary>
        /// Registers adapters for core contracts in the data layer.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterDataAdapters(IContainerBuilder builder)
        {
            builder.Register<DataProvider>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        /// <summary>
        /// Registers adapters for core contracts in the application layer.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterApplicationAdapters(IContainerBuilder builder)
        {

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

            builder.Register<PoolableMessagePublisher<CharacterAnimationMessage>>(Lifetime.Singleton).AsSelf();
            builder.Register<PoolableMessagePublisher<PositionUpdateMessage>>(Lifetime.Singleton).AsSelf();
            builder.Register<PoolableMessagePublisher<SetCharacterStateMessage>>(Lifetime.Singleton).AsSelf();
            
            builder.Register<IMessagePublisher<CharacterAnimationMessage>, MessagePublisher<CharacterAnimationMessage>>(Lifetime.Singleton);
            builder.Register<IMessagePublisher<PositionUpdateMessage>, MessagePublisher<PositionUpdateMessage>>(Lifetime.Singleton);
            builder.Register<IMessagePublisher<SetCharacterStateMessage>, MessagePublisher<SetCharacterStateMessage>>(Lifetime.Singleton);
            
            builder.Register<IMessageSubscriber<CharacterAnimationMessage>, MessageSubscriber<CharacterAnimationMessage>>(Lifetime.Singleton);
            builder.Register<IMessageSubscriber<PositionUpdateMessage>, MessageSubscriber<PositionUpdateMessage>>(Lifetime.Singleton);
            builder.Register<IMessageSubscriber<SetCharacterStateMessage>, MessageSubscriber<SetCharacterStateMessage>>(Lifetime.Singleton);

            builder.Register<IObjectPool<CharacterAnimationMessage>, ObjectPoolAdapter<CharacterAnimationMessage>>(Lifetime.Singleton);
            builder.Register<IObjectPool<PositionUpdateMessage>, ObjectPoolAdapter<PositionUpdateMessage>>(Lifetime.Singleton);
            builder.Register<IObjectPool<SetCharacterStateMessage>, ObjectPoolAdapter<SetCharacterStateMessage>>(Lifetime.Singleton);

            builder.Register<IMessageRegistration, MessageRegistration<CharacterAnimationMessage>>(Lifetime.Singleton);
            builder.Register<IMessageRegistration, MessageRegistration<PositionUpdateMessage>>(Lifetime.Singleton);
            builder.Register<IMessageRegistration, MessageRegistration<SetCharacterStateMessage>>(Lifetime.Singleton);

            builder.Register<IMessageManager, MessageManager>(Lifetime.Singleton);
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
