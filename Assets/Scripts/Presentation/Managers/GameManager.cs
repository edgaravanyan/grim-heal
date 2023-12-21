using UnityEngine;
using VContainer;
using CharacterController = Application.Character.CharacterController;

namespace Presentation.Managers
{
    /// <summary>
    /// Manages the overall game flow, including the main game logic and physics updates.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Inject] private CharacterController characterController;

        /// <summary>
        /// Called once per frame to update the game logic. Delegates logic updates to the character controller.
        /// </summary>
        private void Update()
        {
            // Update the character controller's logical aspects.
            characterController.Update();
        }

        /// <summary>
        /// Called at a fixed time step for physics-related updates. Delegates physics updates to the character controller.
        /// </summary>
        private void FixedUpdate()
        {
            // Update the character controller's physics-related aspects.
            characterController.UpdatePhysics();
        }
    }
}