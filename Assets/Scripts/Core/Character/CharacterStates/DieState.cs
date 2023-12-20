using Assets.Scripts.Core.MessagePipe;

namespace Assets.Scripts.Core.Character.CharacterStates
{
    /// <summary>
    /// Represents the state of a character when it is in the process of dying or has died.
    /// </summary>
    public class DieState : CharacterState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DieState"/> class.
        /// </summary>
        /// <param name="character">The character associated with this state.</param>
        /// <param name="messageManager">The message manager for handling message communication.</param>
        public DieState(Character character, MessageManager messageManager)
            : base(character, messageManager) { }
    }
}