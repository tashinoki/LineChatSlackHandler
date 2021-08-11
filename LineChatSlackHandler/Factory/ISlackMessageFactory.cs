using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Models;
using Line.Messaging.Webhooks;

namespace LineChatSlackHandler.Factory
{
    public interface ISlackMessageFactory
    {
        public IReadOnlyList<SlackMessage> CreateSlackMessages(string lineBotId, IEnumerable<MessageEvent> messageEvents);
    }
}
