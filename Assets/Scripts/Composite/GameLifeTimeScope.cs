using Application.Character;
using Application.Input;
using Application.Managers;
using Application.Map;
using Application.Messages;
using Application.Utils;
using Core.Character;
using Core.Character.CharacterStates;
using Core.Contracts;
using Core.Contracts.Character;
using Core.Contracts.Map;
using Core.Contracts.Messages;
using Core.Contracts.Pool;
using Core.Logger;
using Core.Map;
using Core.MessagePipe;
using Core.MessagePipe.Messages;
using Core.StateMachine;
using Data;
using MessagePipe;
using Presentation.Character;
using Presentation.Managers;
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
        [SerializeField] private MapViewProvider mapViewProvider;

        /// <summary>
        /// Configures the dependencies and their lifetimes for the game.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMessagePipe(builder);
            
            RegisterDataAdapters(builder);
            RegisterPresentationAdapters(builder);
            RegisterApplicationAdapters(builder);

            RegisterInput(builder);
            RegisterMap(builder);
            RegisterCharacter(builder);
        }

        /// <summary>
        /// Registers the MessagePipe and its dependencies.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterMessagePipe(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            RegisterMessage<CharacterAnimationMessage>(builder, options);
            RegisterMessage<PositionUpdateMessage>(builder, options);
            RegisterMessage<SetCharacterStateMessage>(builder, options);
            RegisterMessage<MapChunkMessage>(builder, options);

            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
            
            builder.Register<IMessageManager, MessageManager>(Lifetime.Singleton);
        }

        /// <summary>
        /// Registers components related to handling messages of a specific type.
        /// </summary>
        /// <typeparam name="TMessage">The type of message to be handled.</typeparam>
        /// <param name="builder">The container builder to register dependencies.</param>
        /// <param name="options">The MessagePipe options for registration.</param>
        private void RegisterMessage<TMessage>(IContainerBuilder builder, MessagePipeOptions options) where TMessage : class, IMessage, new()
        {
            builder.RegisterMessageBroker<TMessage>(options);
            builder.Register<PoolableMessagePublisher<TMessage>>(Lifetime.Singleton).AsSelf();
            builder.Register<IMessagePublisher<TMessage>, MessagePublisher<TMessage>>(Lifetime.Singleton);
            builder.Register<IMessageSubscriber<TMessage>, MessageSubscriber<TMessage>>(Lifetime.Singleton);
            builder.Register<IObjectPool<TMessage>, ObjectPoolAdapter<TMessage>>(Lifetime.Singleton);
            builder.Register<IMessageRegistration, MessageRegistration<TMessage>>(Lifetime.Singleton);
        }

        /// <summary>
        /// Registers adapters for core contracts in the data layer.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterDataAdapters(IContainerBuilder builder)
        {
            builder.Register<DataProvider>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PrefabProvider>(Lifetime.Singleton).AsSelf();
        }

        // (Documentation for RegisterViewAdapters method)
        /// <summary>
        /// Registers adapters for core contracts in the presentation layer.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterPresentationAdapters(IContainerBuilder builder)
        {
            builder.RegisterComponent(characterView).As<ICharacterView>();
            builder.RegisterComponent(mapViewProvider).As<IViewProvider<IMapView>>();
        }

        // (Documentation for RegisterApplicationAdapters method)
        /// <summary>
        /// Registers adapters for core contracts in the application layer.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterApplicationAdapters(IContainerBuilder builder)
        {
            LoggerProvider.Initialize(new Logger());
        }

        // (Documentation for RegisterInput method)
        /// <summary>
        /// Registers input dependencies.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        // (Documentation for RegisterMap method)
        /// <summary>
        /// Registers map-related dependencies.
        /// </summary>
        /// <param name="builder">The container builder to register dependencies.</param>
        private void RegisterMap(IContainerBuilder builder)
        {
            builder.Register<MapChunk>(Lifetime.Transient).AsSelf();
            builder.Register<IObjectPool<MapChunk>, ObjectPoolAdapter<MapChunk>>(Lifetime.Singleton);
            
            builder.Register<IMap, Map>(Lifetime.Scoped).AsSelf();
            builder.Register<IMapView, MapView>(Lifetime.Scoped).AsSelf();
            
            builder.Register<IMapController, MapController>(Lifetime.Scoped).AsImplementedInterfaces();
        }

        // (Documentation for RegisterCharacter method)
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

            builder.Register<CharacterMovementController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<CharacterAnimationController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<CharacterStateRunner>(Lifetime.Singleton).As<StateRunner<CharacterState>>();
            builder.RegisterEntryPoint<CharacterController>(Lifetime.Scoped).AsSelf();
        }
    }
}
