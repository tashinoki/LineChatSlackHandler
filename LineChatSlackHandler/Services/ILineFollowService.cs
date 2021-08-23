using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Models;
using System.Threading.Tasks;
using Line.Messaging.Webhooks;

namespace LineChatSlackHandler.Services
{
    public interface ILineFollowService
    {
        public Task<LineWebhookHandleResult> MakeChannelMappingConfigAsync(string botId, FollowEvent followEvent);
    }
}
