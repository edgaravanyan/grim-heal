using System;
using Application.Input;
using Assets.Scripts.Core.Character.CharacterStates;
using Assets.Scripts.Core.MessagePipe.Messages;
using Assets.Scripts.Core.StateMachine;
using MessagePipe;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Application.Character
{
    /// <summary>
    /// Handles character movement input and updates the character state accordingly.
    /// </summary>
    public class CharacterMovementController : IInitializable, IDisposable
    {
        [Inject] private InputActions input;
        [Inject] private StateRunner<CharacterState> characterStateRunner;
        [Inject] private ICharacterView characterView;
        [Inject] private ISubscriber<PositionUpdateMessage> messageSubscriber;

        /// <summary>
        /// Initializes the character movement controller by subscribing to movement input events.
        /// </summary>
        void IInitializable.Initialize()
        {
            input.Game.Movement.performed += CaptureInput;
            input.Game.Movement.canceled += CaptureInput;
            messageSubscriber.Subscribe(message => characterView.SetPosition(new Vector2(message.data.X, message.data.Y)));
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
        /// Unsubscribes from movement input events when the character movement controller is disposed.
        /// </summary>
        void IDisposable.Dispose()
        {
            input.Game.Movement.performed -= CaptureInput;
            input.Game.Movement.canceled -= CaptureInput;
        }
    }
}