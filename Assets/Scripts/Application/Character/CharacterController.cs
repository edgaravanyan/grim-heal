using System;
using Application.Input;
using Core.Character.CharacterStates;
using Core.Contracts;
using Core.Contracts.Messages;
using Core.MessagePipe.Messages;
using Core.StateMachine;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Application.Character
{
    /// <summary>
    /// Controls and updates the character.
    /// </summary>
    public class CharacterController : ICharacterController, IStartable, IInitializable, IDisposable
    {
        [Inject] private DataProvider dataProvider;
        [Inject] private Core.Character.Character character;
        [Inject] private StateRunner<CharacterState> characterStateRunner;
        [Inject] private IMessageManager messageManager;
        [Inject] private InputActions input;

        /// <summary>
        /// Initializes the CharacterController by asynchronously retrieving character stats and performing additional setup.
        /// </summary>
        async void IInitializable.Initialize()
        {
            input.Game.Movement.performed += CaptureInput;
            input.Game.Movement.canceled += CaptureInput;
            
            // Asynchronously initialize character stats.
            character.CharacterData = await dataProvider.GetCharacterDataAsync();
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
        public void Update()
        {
            // Update logical aspects of the current state.
            characterStateRunner.UpdateCurrentState(Time.deltaTime);
        }

        /// <summary>
        /// Handles the character update during fixed time steps for physics.
        /// </summary>
        public void UpdatePhysics()
        {
            // Update physics-related aspects of the current state.
            characterStateRunner.FixedUpdate(Time.fixedDeltaTime);
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
        /// Unsubscribes from movement input events when the character controller is disposed.
        /// </summary>
        void IDisposable.Dispose()
        {
            input.Game.Movement.performed -= CaptureInput;
            input.Game.Movement.canceled -= CaptureInput;
        }

        /// <summary>
        /// Sets the character state based on the received message.
        /// </summary>
        private void SetCharacterState(SetCharacterStateMessage message)
        {
            characterStateRunner.SetState(message.Data);
        }
    }
}
