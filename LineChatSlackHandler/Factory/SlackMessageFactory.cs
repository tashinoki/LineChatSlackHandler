using Line.Messaging.Webhooks;
using LineChatSlackHandler.Entity;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LineChatSlackHandler.Factory
{
    public class SlackMessageFactory: ISlackMessageFactory
    {
        private readonly IChannelMappingConfigRepository _mappingConfigRepository;

        public SlackMessageFactory(IChannelMappingConfigRepository channelMappingConfigRepository)
        {
            _mappingConfigRepository = channelMappingConfigRepository;
        }

        public async Task<SlackMessage> CreateSlackMessageAsync(string lineBotId, MessageEvent messageEvent)
        {
            var mappingConfig = await _mappingConfigRepository.GetWithLineUserIdAsync(messageEvent.Source.UserId, lineBotId);
            var slackMessage = CreateSlackMessage(mappingConfig, messageEvent);

            return slackMessage;
        }

        private SlackMessage CreateSlackMessage(ChannelMappingConfig mappingConfig, MessageEvent messageEvent)
        {
            switch(messageEvent.Message.Type)
            {
                case EventMessageType.Text:

                    return new SlackTextMessage {
                        Channel = mappingConfig.SlackChannelId,
                        Text = AttatchMention((messageEvent.Message as TextEventMessage)?.Text)
                    };
                default:
                    throw new Exception($"無効なLine Webhook Event {messageEvent.Message.Type} が指定されました。");
            }

            #region LocalFunction
            // Todo: remove hard coding
            string AttatchMention(string text) => $"<@U0226MT50F6>\n{text}";
            #endregion
        }
    }
}
