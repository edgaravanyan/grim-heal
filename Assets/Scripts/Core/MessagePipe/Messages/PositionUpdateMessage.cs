using System.Numerics;

namespace Core.MessagePipe.Messages
{
    /// <summary>
    /// Represents a message indicating an update to the position.
    /// </summary>
    public class PositionUpdateMessage : Message<Vector2> { }
}