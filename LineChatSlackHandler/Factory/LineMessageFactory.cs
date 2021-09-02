using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Repository;
using Line.Messaging;
using LineChatSlackHandler.Services;

namespace LineChatSlackHandler.Factory
{
    public class LineMessageFactory: ILineMessageFactory
    {
        private IChannelMappingConfigRepository _mappongConfiguRepository;

        public LineMessageFactory(IChannelMappingConfigRepository channelMappingConfigRepository)
        {
            _mappongConfiguRepository = channelMappingConfigRepository;
        }

        public async Task<LineMessage> CreateAsync(SlackEvent slackEvent)
        {
            var mappingConfig = await _mappongConfiguRepository.GetWithSlackChannelIdAsync(slackEvent.Channel);

            switch(slackEvent.Subtype)
            {
                case SlackEventSubType.Text:
                    return new LineMessage {
                        ToUserId = mappingConfig.LineUserId,
                        Message = new TextMessage(slackEvent.Text),
                    };
                default:
                    throw new ArgumentException("不適切な Slack Event です。");
            }
        }
    }
}
