using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Models;
using Line.Messaging.Webhooks;
using System.Threading.Tasks;

namespace LineChatSlackHandler.Factory
{
    public interface ISlackMessageFactory
    {
        public Task<IReadOnlyList<SlackMessage>> CreateSlackMessages(string lineBotId, IEnumerable<MessageEvent> messageEvents);
    }
}
