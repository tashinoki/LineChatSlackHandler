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
        private ISlackService _slackService;

        public LineFollowService(IChannelMappingConfigRepository mappingConfigRepository, ISlackService slackService)
        {
            _mappingConfigRepository = mappingConfigRepository;
            _slackService = slackService;
        }

        public async Task<LineWebhookHandleResult> MakeChannelMappingConfigAsync(string botId, FollowEvent followEvent)
        {
            var slackChannelId = await _slackService.CreateChannelAsync(followEvent.Source.UserId.ToLower());
            return new LineWebhookHandleResult { };
        }
    }
}
