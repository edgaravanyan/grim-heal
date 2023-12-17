using System;

namespace Assets.Scripts.Core.MessagePipe.Messages
{
    /// <summary>
    /// A message class for signaling the need to play a character animation.
    /// </summary>
    public class CharacterAnimationMessage : Message<Type>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterAnimationMessage"/> class.
        /// </summary>
        public CharacterAnimationMessage() { }
    }
}