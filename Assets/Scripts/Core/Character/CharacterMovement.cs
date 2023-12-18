using System;
using System.Numerics;
using Assets.Scripts.Core.Logger;

namespace Assets.Scripts.Core.Character
{
    /// <summary>
    /// Manages the movement of the character, including speed calculation and movement.
    /// </summary>
    public class CharacterMovement
    {
        private readonly Character character;

        private readonly float acceleration = 10;
        private readonly float deAcceleration;
        private readonly float moveSpeed = 3;
        private float currentSpeed = 0;

        public CharacterMovement(Character character)
        {
            this.character = character;
            // acceleration = character.CharacterStats.Acceleration;
            // deAcceleration = character.CharacterStats.DeAcceleration;
            // moveSpeed = character.CharacterStats.MoveSpeed;
        }

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
                currentSpeed += acceleration * moveSpeed * deltaTime;
            }
            else
            {
                currentSpeed -= deAcceleration * moveSpeed * deltaTime;
            }

            // Clamp the speed to ensure it stays within the specified range.
            currentSpeed = Math.Clamp(currentSpeed, 0, moveSpeed);
        }
    }
}