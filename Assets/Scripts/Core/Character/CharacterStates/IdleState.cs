using Core.Contracts.Messages;
using Core.MessagePipe.Messages;

namespace Core.Character.CharacterStates
{
    /// <summary>
    /// Represents the idle state of the character.
    /// </summary>
    public class IdleState : CharacterState
    {
        public IdleState(Character character, IMessageManager messageManager)
            : base(character,messageManager) { }
        
        /// <summary>
        /// Checks for a change to the WalkState based on movement input.
        /// </summary>
        public override void CheckToChange()
        {
            base.CheckToChange();

            // Check if there is movement input, and transition to the WalkState if true.
            if (Input.LengthSquared() > 0)
            {
                messageManager.Publish<SetCharacterStateMessage>(typeof(WalkState));
            }
        }
    }
}