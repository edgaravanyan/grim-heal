using UnityEngine;
using VContainer;
using CharacterController = Application.Character.CharacterController;

namespace Application.Managers
{
    /// <summary>
    /// Manages the overall game flow and dependencies.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Inject] private CharacterController characterController;

        /// <summary>
        /// Called once per frame to update game logic.
        /// </summary>
        private void Update()
        {
            // Update the character controller.
            characterController.Update();
        }
    }
}