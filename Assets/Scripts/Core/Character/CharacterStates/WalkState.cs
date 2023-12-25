using Core.Contracts.Messages;
using Core.MessagePipe.Messages;

namespace Core.Character.CharacterStates
{
    /// <summary>
    /// Represents the walking state of a game character. Inherits from abstract CharacterState.
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
        /// Updates the logical aspects of the walking state.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last frame.</param>
        public override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);
            characterMovement.Move(deltaTime, Input);
            messageManager.Publish<PositionUpdateMessage>(character.Position);
        }

        /// <summary>
        /// Checks to change to the IdleState based on the movement speed.
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