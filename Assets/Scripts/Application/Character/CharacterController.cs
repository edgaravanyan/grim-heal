using Assets.Scripts.Core.Character.CharacterStates;
using Assets.Scripts.Core.MessagePipe.Messages;
using Assets.Scripts.Core.StateMachine;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application.Character
{
    /// <summary>
    /// Responsible for updating the current state of the character.
    /// </summary>
    public class CharacterController : IStartable
    {
        [Inject] private StateRunner<CharacterState> characterStateRunner;
        [Inject] private ISubscriber<SetCharacterStateMessage> stateChangeSubscriber;

        /// <summary>
        /// Initializes the character controller by setting the initial state to IdleState.
        /// </summary>
        void IStartable.Start()
        {
            characterStateRunner.SetState(typeof(IdleState));
            stateChangeSubscriber.Subscribe(message =>
            {
                characterStateRunner.SetState(message.data);
            });
        }

        /// <summary>
        /// Update method called per frame to handle the character's state updates.
        /// </summary>
        public void Update()
        {
            // Update the current state's logical aspects.
            characterStateRunner.UpdateCurrentState(Time.deltaTime);
        }

        /// <summary>
        /// Physics update method called at a fixed time step to handle the character's state updates.
        /// </summary>
        public void UpdatePhysics()
        {
            // Update the current state's physics-related aspects.
            characterStateRunner.FixedUpdate(Time.fixedDeltaTime);
        }
    }
}