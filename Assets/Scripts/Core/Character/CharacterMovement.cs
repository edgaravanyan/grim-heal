using System;
using System.Numerics;

namespace Core.Character
{
    /// <summary>
    /// Manages the movement of the character, including speed calculation and movement.
    /// </summary>
    public class CharacterMovement
    {
        private readonly Character character;

        private float acceleration;
        private float deAcceleration;
        private float moveSpeed;
        private Vector2 moveDirection;
        
        public float CurrentSpeed { get; private set; }

        public CharacterMovement(Character character)
        {
            this.character = character;
            this.character.OnStatsUpdated += stats =>
            {
                acceleration = stats.Acceleration;
                deAcceleration = stats.DeAcceleration;
                moveSpeed = stats.MoveSpeed;
            };
        }

        /// <summary>
        /// Moves the character based on the current movement parameters.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        /// <param name="moveDirection">The direction of movement (input).</param>
        public void Move(float deltaTime, Vector2 moveDirection)
        {
            CalculateSpeed(deltaTime, moveDirection);
            MoveBySpeed(deltaTime);
        }
        
        private void CalculateSpeed(float deltaTime, Vector2 direction)
        {
            if (direction.LengthSquared() > 0 && CurrentSpeed >= 0)
            {
                moveDirection = direction;
                CurrentSpeed += acceleration * moveSpeed * deltaTime;
            }
            else
            {
                CurrentSpeed -= deAcceleration * moveSpeed * deltaTime;
            }
            CurrentSpeed = Math.Clamp(CurrentSpeed, 0, moveSpeed);
        }

        private void MoveBySpeed(float deltaTime)
        {
            var currentPosition = character.Position;
            var destination = currentPosition + CurrentSpeed * deltaTime * moveDirection;
            character.Position = destination;
        }
    }
}