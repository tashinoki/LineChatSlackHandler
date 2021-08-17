using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Repository;
using Line.Messaging.Webhooks;
using System.Threading.Tasks;
using LineChatSlackHandler.Models;

namespace LineChatSlackHandler.Services
{
    public class LineFollowService: ILineFollowService
    {
        private IChannelMappingConfigRepository _mappingConfigRepository;

        public LineFollowService(IChannelMappingConfigRepository mappingConfigRepository)
        {
            _mappingConfigRepository = mappingConfigRepository;
        }

        public async Task<LineWebhookHandleResult> MakeChannelMappingConfigAsync(string botId, FollowEvent followEvent)
        {
            return new LineWebhookHandleResult { };
        }
    }
}
