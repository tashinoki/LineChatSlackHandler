using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Repository;
using Line.Messaging;

namespace LineChatSlackHandler.Factory
{
    public class LineMessageFactory: ILineMessageFactory
    {
        private IChannelMappingConfigRepository _mappongConfiguRepository;

        public LineMessageFactory(IChannelMappingConfigRepository channelMappingConfigRepository)
        {
            _mappongConfiguRepository = channelMappingConfigRepository;
        }

        public LineMessage Create(SlackEvent slackEvent)
        {
            var mappingConfig = _mappongConfiguRepository.GetWithSlackChannelId(slackEvent.Channel);

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
