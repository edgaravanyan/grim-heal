using System;
using System.Numerics;
using VContainer;

namespace Assets.Scripts.Core.Character
{
    /// <summary>
    /// Manages the movement of the character, including speed calculation and movement.
    /// </summary>
    public class CharacterMovement
    {
        [Inject] private Character character;

        private float currentSpeed = 0;
        private float acceleration = 2;
        private float deAcceleration = 1;
        private float speed = 2;

        /// <summary>
        /// Moves the character based on the current movement parameters.
        /// </summary>
        /// <param name="fixedDeltaTime">The fixed time step for physics calculations.</param>
        /// <param name="moveDirection">The direction of movement (input).</param>
        public void Move(float fixedDeltaTime, Vector2 moveDirection)
        {
            // Update the character's position based on the current speed and movement direction.
            Vector2 position = new Vector2(character.Position.X, character.Position.Y);
            position += currentSpeed * fixedDeltaTime * moveDirection;
            character.Position.Set(position);
        }

        /// <summary>
        /// Calculates the speed of the character based on the current input and acceleration.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        /// <param name="moveDirection">The direction of movement (input).</param>
        public void CalculateSpeed(float deltaTime, Vector2 moveDirection)
        {
            // Adjust the character's speed based on acceleration and deceleration.
            if (moveDirection.LengthSquared() > 0 && currentSpeed >= 0)
            {
                currentSpeed += acceleration * speed * deltaTime;
            }
            else
            {
                currentSpeed -= deAcceleration * speed * deltaTime;
            }

            // Clamp the speed to ensure it stays within the specified range.
            currentSpeed = Math.Clamp(currentSpeed, 0, speed);
        }
    }
}