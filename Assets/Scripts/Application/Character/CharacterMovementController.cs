using Core.Contracts;
using Core.Contracts.Character;
using Core.Contracts.Messages;
using Core.MessagePipe.Messages;
using VContainer;
using VContainer.Unity;

namespace Application.Character
{
    /// <summary>
    /// Handles character movement
    /// </summary>
    public class CharacterMovementController : IInitializable
    {
        [Inject] private IAnimatedView animatedView;
        [Inject] private IMessageManager messageManager;

        /// <summary>
        /// Initializes the character movement controller by subscribing to movement input events.
        /// </summary>
        void IInitializable.Initialize()
        {
            messageManager.Subscribe<PositionUpdateMessage>(message => animatedView.SetPosition(message.Data));
        }
    }
}