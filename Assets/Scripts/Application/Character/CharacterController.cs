using Core.Character.CharacterStates;
using Core.Contracts.Messages;
using Core.MessagePipe.Messages;
using Core.StateMachine;
using Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.Character
{
    /// <summary>
    /// Controls and updates the state of the character.
    /// </summary>
    public class CharacterController : IStartable, IInitializable
    {
        [Inject] private DataProvider dataProvider;
        [Inject] private Core.Character.Character character;
        [Inject] private StateRunner<CharacterState> characterStateRunner;
        [Inject] private IMessageManager messageManager;

        /// <summary>
        /// Initializes the CharacterController by asynchronously retrieving character stats and performing additional setup.
        /// </summary>
        async void IInitializable.Initialize()
        {
            // Asynchronously initialize character stats.
            character.CharacterStats = await dataProvider.GetCharacterStatsAsync();
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
        /// Handles the character state update during each frame.
        /// </summary>
        public void Update()
        {
            // Update logical aspects of the current state.
            characterStateRunner.UpdateCurrentState(Time.deltaTime);
        }

        /// <summary>
        /// Handles the character state update during fixed time steps for physics.
        /// </summary>
        public void UpdatePhysics()
        {
            // Update physics-related aspects of the current state.
            characterStateRunner.FixedUpdate(Time.fixedDeltaTime);
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
