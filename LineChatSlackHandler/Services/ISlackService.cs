using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Line.Messaging.Webhooks;

namespace LineChatSlackHandler.Services
{
    public interface ISlackService
    {
        public Task SendMessage(IReadOnlyList<MessageEvent> messageEvents);
    }
}
