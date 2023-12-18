using Assets.Scripts.Core.Contracts;
using Assets.Scripts.Core.MessagePipe;
using Assets.Scripts.Core.MessagePipe.Messages;
using VContainer;

namespace Assets.Scripts.Core.Character.CharacterStates
{
    /// <summary>
    /// Represents the walking state of a game character. Inherits from CharacterState.
    /// </summary>
    public class WalkState : CharacterState
    {
        [Inject] protected CharacterMovement characterMovement;
        [Inject] protected PoolableMessagePublisher<PositionUpdateMessage, IPosition> positionPublisher;

        /// <summary>
        /// Updates the logical aspects of the walking state, including calculating character speed.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);
            characterMovement.CalculateSpeed(deltaTime, Input);
        }

        /// <summary>
        /// Updates the physics for the walking state, including moving the character.
        /// </summary>
        /// <param name="fixedDeltaTime">The fixed time step for physics calculations.</param>
        public override void UpdatePhysics(float fixedDeltaTime)
        {
            base.UpdatePhysics(fixedDeltaTime);
            characterMovement.Move(fixedDeltaTime, Input);
            positionPublisher.Publish(character.Position);
        }

        /// <summary>
        /// Checks for a change to the IdleState based on movement input.
        /// </summary>
        public override void CheckToChange()
        {
            base.CheckToChange();

            // Check if there is no movement input, and transition to the IdleState if true.
            if (Input.LengthSquared() == 0)
            {
                setStatePublisher.Publish(typeof(IdleState));
            }
        }
    }
}