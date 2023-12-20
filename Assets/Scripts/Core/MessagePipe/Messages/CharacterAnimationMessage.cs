using System;

namespace Core.MessagePipe.Messages
{
    /// <summary>
    /// A message class for signaling the need to play a character animation.
    /// </summary>
    public class CharacterAnimationMessage : Message<Type> { }
}