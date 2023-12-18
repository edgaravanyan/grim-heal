using Application.Character;
using Assets.Scripts.Core.Contracts;
using UnityEngine;

namespace View.Character
{
    /// <summary>
    /// Represents the visual view of a character in the game.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CharacterView : MonoBehaviour, ICharacterView
    {
        private Animator animator;

        /// <summary>
        /// Initializes the Animator component during Awake.
        /// </summary>
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Plays the specified animation trigger.
        /// </summary>
        /// <param name="trigger">The trigger parameter for the animation.</param>
        public void PlayAnimation(int trigger)
        {
            // Set the trigger to play the animation.
            animator.SetTrigger(trigger);
        }

        /// <summary>
        /// Sets the position of the character view in the game world.
        /// </summary>
        /// <param name="position">The new position for the character.</param>
        public void SetPosition(IPosition position)
        {
            // Set the GameObject's position based on the provided coordinates.
            transform.position = new Vector2(position.X, position.Y);
        }
    }
}