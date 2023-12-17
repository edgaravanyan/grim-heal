using Core.Character.CharacterStates;
using Core.StateMachine;
using VContainer;
using VContainer.Unity;

namespace Application.Character
{
    /// <summary>
    /// Responsible for updating the current state of the character.
    /// </summary>
    public class CharacterController : IStartable
    {
        [Inject] private CharacterAnimationController characterAnimationController;
        [Inject] private StateRunner<CharacterState> characterStateRunner;

        void IStartable.Start()
        {
            characterAnimationController.Initialize();
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