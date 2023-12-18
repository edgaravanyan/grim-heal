using System;
using Assets.Scripts.Core.MessagePipe;
using Assets.Scripts.Core.MessagePipe.Messages;

namespace Assets.Scripts.Core.Character.CharacterStates
{
    public class DieState : CharacterState
    {
        public DieState(Character character,
            PoolableMessagePublisher<CharacterAnimationMessage, Type> animationPublisher,
            PoolableMessagePublisher<SetCharacterStateMessage, Type> setStatePublisher)
            : base(character,
                animationPublisher,
                setStatePublisher) { }
    }
}
