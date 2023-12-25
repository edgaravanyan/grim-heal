using Core.Contracts.Messages;
using Core.MessagePipe.Messages;

namespace Core.Character.CharacterStates
{
    /// <summary>
    /// Represents the walking state of a game character. Inherits from CharacterState.
    /// </summary>
    public class WalkState : CharacterState
    {
        private CharacterMovement characterMovement;

        public WalkState(Character character,
            CharacterMovement characterMovement,
            IMessageManager messageManager)
        :base(character, messageManager)
        {
            this.characterMovement = characterMovement;
        }
        
        /// <summary>
        /// Updates the logical aspects of the walking state, including calculating character speed.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);
            characterMovement.Move(deltaTime, Input);
            messageManager.Publish<PositionUpdateMessage>(character.Position);
        }

        /// <summary>
        /// Checks for a change to the IdleState based on movement input.
        /// </summary>
        public override void CheckToChange()
        {
            base.CheckToChange();
            if (characterMovement.CurrentSpeed == 0)
            {
                messageManager.Publish<SetCharacterStateMessage>(typeof(IdleState));
            }
        }
    }
}