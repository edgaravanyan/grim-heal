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

        private void Update()
        {
            // Update the character controller's logical aspects.
            characterController.Update();
        }

        private void FixedUpdate()
        {
            // Update the character controller's physics-related aspects.
            characterController.UpdatePhysics();
        }
    }
}