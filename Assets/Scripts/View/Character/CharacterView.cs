using Application.Character;
using UnityEngine;

namespace View.Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterView : MonoBehaviour, ICharacterView
    {
        private Animator animator;

        private void Awake()
        {
            // Initialize the Animator component.
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Plays the animation.
        /// </summary>
        public void PlayAnimation(int trigger)
        {
            // Set the trigger to play the animation.
            animator.SetTrigger(trigger);
        }
    }
}