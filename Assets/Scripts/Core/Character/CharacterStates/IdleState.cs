namespace Assets.Scripts.Core.Character.CharacterStates
{
    /// <summary>
    /// Represents the idle state of the character.
    /// </summary>
    public class IdleState : CharacterState
    {
        /// <summary>
        /// Checks for a change to the WalkState based on movement input.
        /// </summary>
        public override void CheckToChange()
        {
            base.CheckToChange();

            // Check if there is movement input, and transition to the WalkState if true.
            if (Input.LengthSquared() > 0)
            {
                setStatePublisher.Publish(typeof(WalkState));
            }
        }
    }
}