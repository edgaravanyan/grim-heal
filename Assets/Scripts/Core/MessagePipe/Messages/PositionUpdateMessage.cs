using Assets.Scripts.Core.Contracts;

namespace Assets.Scripts.Core.MessagePipe.Messages
{
    /// <summary>
    /// Represents a message indicating an update to the position.
    /// </summary>
    public class PositionUpdateMessage : Message<IPosition> { }
}