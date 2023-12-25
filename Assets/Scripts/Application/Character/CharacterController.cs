using System;
using Application.Input;
using Core.Character.CharacterStates;
using Core.Contracts.Character;
using Core.Contracts.Messages;
using Core.MessagePipe.Messages;
using Core.StateMachine;
using Data;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Application.Character
{
    /// <summary>
    /// Controls and updates the character.
    /// </summary>
    public class CharacterController : ICharacterController, IInitializable, IStartable, ITickable, IFixedTickable, IDisposable
    {
        [Inject] private Core.Character.Character character;
        [Inject] private StateRunner<CharacterState> characterStateRunner;
        [Inject] private DataProvider dataProvider;
        [Inject] private IMessageManager messageManager;
        [Inject] private InputActions inputActions;
        
        private IDisposable clickStream;

        /// <summary>
        /// Initializes the CharacterController by asynchronously retrieving character stats and performing additional setup.
        /// </summary>
        void IInitializable.Initialize()
        {
            SetCharacterDataAsync();
            RegisterCharacterInput();
        }

        /// <summary>
        /// Starts the character controller by setting the initial state to IdleState.
        /// </summary>
        void IStartable.Start()
        {
            // Set the initial state to IdleState.
            characterStateRunner.SetState<IdleState>();
            messageManager.Subscribe<SetCharacterStateMessage>(SetCharacterState);
        }

        /// <summary>
        /// Handles the character update during each frame.
        /// </summary>
        public void Tick()
        {
            // Update logical aspects of the current state.
            characterStateRunner.UpdateCurrentState(Time.deltaTime);
        }

        /// <summary>
        /// Handles the character update during fixed time steps for physics.
        /// </summary>
        public void FixedTick()
        {
            // Update physics-related aspects of the current state.
            characterStateRunner.FixedUpdate(Time.fixedDeltaTime);
        }

        private async void SetCharacterDataAsync()
        {
            character.CharacterData = await dataProvider.GetCharacterDataAsync();
        }

        private void RegisterCharacterInput()
        {
            var gameMovement = inputActions.Game.Movement;
            var performedObservable = Observable.FromEvent<InputAction.CallbackContext>(
                h => gameMovement.performed += h,
                h => gameMovement.performed -= h
            );
            var canceledObservable = Observable.FromEvent<InputAction.CallbackContext>(
                h => gameMovement.canceled += h,
                h => gameMovement.canceled -= h
            );
            clickStream = performedObservable.Merge(canceledObservable).Subscribe(CaptureInput);
        }

        /// <summary>
        /// Captures movement input and updates the character state with the input vector.
        /// </summary>
        /// <param name="context">The InputAction.CallbackContext containing the input data.</param>
        private void CaptureInput(InputAction.CallbackContext context)
        {
            // Read the movement input vector from the input context and pass it to the character state runner.
            var movementInput = context.ReadValue<Vector2>();
            var vector2 = new System.Numerics.Vector2(movementInput.x, movementInput.y);
            characterStateRunner.HandleInput(vector2);
        }

        /// <summary>
        /// Sets the character state based on the received message.
        /// </summary>
        private void SetCharacterState(SetCharacterStateMessage message)
        {
            characterStateRunner.SetState(message.Data);
        }

        /// <summary>
        /// Unsubscribes from movement input events when the character controller is disposed.
        /// </summary>
        void IDisposable.Dispose()
        {
            clickStream.Dispose();
        }
    }
}
