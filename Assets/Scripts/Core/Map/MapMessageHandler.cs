using System;
using Core.Contracts.Messages;
using Core.MessagePipe.Messages;

namespace Core.Map
{
    /// <summary>
    /// Handles message subscriptions and reacts to updates.
    /// </summary>
    public class MapMessageHandler
    {
        private readonly IMessageManager messageManager;

        /// <summary>
        /// Initializes a new instance of the MapMessageHandler class.
        /// </summary>
        /// <param name="messageManager">The message manager for handling inter-object communication.</param>
        public MapMessageHandler(IMessageManager messageManager)
        {
            this.messageManager = messageManager;
        }

        /// <summary>
        /// Subscribes to position update messages with the provided handler action.
        /// </summary>
        /// <param name="handler">The action to be executed upon receiving a position update message.</param>
        public void SubscribeToPositionUpdates(Action<PositionUpdateMessage> handler)
        {
            messageManager.Subscribe(handler);
        }

        /// <summary>
        /// Publishes a map chunk update message.
        /// </summary>
        /// <param name="chunk">The map chunk to be included in the update message.</param>
        public void PublishMapChunkUpdate(MapChunk chunk)
        {
            messageManager.Publish<MapChunkMessage>(chunk);
        }
    }
}