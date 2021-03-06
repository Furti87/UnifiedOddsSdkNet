/*
* Copyright (C) Sportradar AG. See LICENSE for full license governing this code
*/
using System;
using System.Diagnostics.Contracts;
using Sportradar.OddsFeed.SDK.Entities.Internal.EventArguments;
using Sportradar.OddsFeed.SDK.Messages.Internal.Feed;

namespace Sportradar.OddsFeed.SDK.Entities.Internal
{
    /// <summary>
    /// Base class for message processor
    /// </summary>
    public abstract class MessageProcessorBase {

        /// <summary>
        /// Raised when a <see cref="FeedMessage"/> instance has been processed
        /// </summary>
        public virtual event EventHandler<FeedMessageReceivedEventArgs> MessageProcessed;

        /// <summary>
        /// Raise a <see cref="MessageProcessed"/> event
        /// </summary>
        /// <param name="e">Send the <see cref="FeedMessage"/> originally received</param>
        protected void RaiseOnMessageProcessedEvent(FeedMessageReceivedEventArgs e)
        {
            Contract.Requires(e != null);

            MessageProcessed?.Invoke(this, e);
        }
    }
}