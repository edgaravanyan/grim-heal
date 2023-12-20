using System;

namespace Assets.Scripts.Core.Contracts.Messages
{
    /// <summary>
    /// Represents the registration information for a message publisher or subscriber.
    /// </summary>
    public interface IMessageRegistration
    {
        /// <summary>
        /// Gets the type of the message that the publisher or subscriber is associated with.
        /// </summary>
        Type MessageType { get; }

        /// <summary>
        /// Gets the instance of the message publisher.
        /// </summary>
        object Publisher { get; }

        /// <summary>
        /// Gets the instance of the message subscriber.
        /// </summary>
        object Subscriber { get; }
    }
}