﻿using System;
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
        private ISlackChannelService _slackChannelService;

        public LineFollowService(IChannelMappingConfigRepository mappingConfigRepository, ISlackChannelService slackChannelService)
        {
            _mappingConfigRepository = mappingConfigRepository;
            _slackChannelService = slackChannelService;
        }

        public async Task<LineWebhookHandleResult> MakeChannelMappingConfigAsync(string botId, FollowEvent followEvent)
        {
            var lineUserId = followEvent.Source.UserId;
            var mappingConfig = await _mappingConfigRepository.GetWithLineUserIdAsync(botId, lineUserId);

            if (mappingConfig is null)
            {
                var slackChannel = await _slackChannelService.StartConversationAsync(lineUserId);
                var config = await _mappingConfigRepository.CreateAsync(botId, lineUserId, slackChannel.Id);
            }
            else if (mappingConfig.IsDeleted)
            {
                mappingConfig.IsDeleted = false;
                var config = await _mappingConfigRepository.UpdateAsync(config);
            }
            return new LineWebhookHandleResult { };
        }
    }
}
