using Application.Character;

namespace Core.MessagePipe.Messages
{
    /// <summary>
    /// A message class for signaling the need to play a character animation.
    /// </summary>
    public class CharacterAnimationMessage : Message<CharacterAnimationMessage, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterAnimationMessage"/> class.
        /// </summary>
        public CharacterAnimationMessage() { }

        /// <summary>
        /// Gets the hash of the animation to be played.
        /// </summary>
        public int AnimationHash { get; private set; }

        /// <summary>
        /// Initializes the animation message for reuse.
        /// </summary>
        /// <param name="animationHash">The hash of the animation to be played (get from <see cref="CharacterAnimationController"/>).</param>
        public override void Initialize(int animationHash)
        {
            AnimationHash = animationHash;
        }
    }
}