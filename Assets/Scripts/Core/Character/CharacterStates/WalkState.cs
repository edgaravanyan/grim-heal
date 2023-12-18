using VContainer;

namespace Assets.Scripts.Core.Character.CharacterStates
{
    /// <summary>
    /// Represents the walking state of a game character. Inherits from CharacterState.
    /// </summary>
    public class WalkState : CharacterState
    {
        [Inject] protected CharacterMovement characterMovement;

        /// <summary>
        /// Updates the logical aspects of the walking state, including calculating character speed.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);

            // Calculate character speed based on the current walking state.
            characterMovement.CalculateSpeed(deltaTime);
        }

        /// <summary>
        /// Updates the physics for the walking state, including moving the character.
        /// </summary>
        /// <param name="fixedDeltaTime">The fixed time step for physics calculations.</param>
        public override void UpdatePhysics(float fixedDeltaTime)
        {
            base.UpdatePhysics(fixedDeltaTime);

            // Move the character based on the physics of the walking state.
            characterMovement.Move(fixedDeltaTime);
        }
    }
}