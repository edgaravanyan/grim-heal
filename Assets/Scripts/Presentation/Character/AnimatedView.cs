using Core.Contracts.Character;
using UnityEngine;

namespace Presentation.Character
{
    /// <summary>
    /// Represents the visual view of an entity in the game.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class AnimatedView : MonoBehaviour, IAnimatedView
    {
        private Animator animator;
        private Rigidbody2D rigidBody;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Plays the specified animation by trigger.
        /// </summary>
        /// <param name="trigger">The trigger parameter for the animation.</param>
        public void PlayAnimation(int trigger)
        {
            // Set the trigger to play the animation.
            animator.SetTrigger(trigger);
        }

        /// <summary>
        /// Sets the position of the entity view in the game world.
        /// </summary>
        /// <param name="position">The new position for the entity.</param>
        public void SetPosition(System.Numerics.Vector2 position)
        {
            transform.position = new Vector2(position.X, position.Y);
        }
    }
}