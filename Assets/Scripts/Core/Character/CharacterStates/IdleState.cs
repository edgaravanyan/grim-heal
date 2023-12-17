namespace Assets.Scripts.Core.Character.CharacterStates
{
    /// <summary>
    /// Represents the idle state of the character.
    /// </summary>
    public class IdleState : CharacterState
    {
        /// <summary>
        /// Called when entering the idle state.
        /// </summary>
        public override void Enter()
        {
            base.Enter();
            
            // Publish a message to play the idle animation.
            animationPublisher.Publish(this.GetType());
        }
    }
}