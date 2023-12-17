using Assets.Scripts.Core.Character.CharacterStates;
using Assets.Scripts.Core.StateMachine;
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

        void IStartable.Start()
        {
            characterStateRunner.SetState(typeof(IdleState));
        }

        /// <summary>
        /// Update method called per frame to handle the character's state updates.
        /// </summary>
        public void Update()
        {
            characterStateRunner.UpdateCurrentState();
        }
    }
}