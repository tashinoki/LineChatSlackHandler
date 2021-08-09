using System;
using System.Collections.Generic;
using System.Linq;
using LineChatSlackHandler.Models;
using Line.Messaging;
using Line.Messaging.Webhooks;
using LineChatSlackHandler.Repository;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Factory
{
    public class ChannelMessageFactory: IChannelMessageFactory
    {
        private readonly IChannelMappingConfigRepository _mappingConfigRepository;

        public ChannelMessageFactory(IChannelMappingConfigRepository channelMappingConfigRepository)
        {
            _mappingConfigRepository = channelMappingConfigRepository;
        }

        public IReadOnlyList<SlackMessage> CreateSlackMessages(string lineBotId, IEnumerable<MessageEvent> messageEvents)
        {
            IList<SlackMessage> messages = new List<SlackMessage>();

            foreach(var messageEvent in messageEvents)
            {
                var mappingConfig = _mappingConfigRepository.GetWithLineUserId(messageEvent.Source.UserId, lineBotId);
                var slackMessage = CreateSlackMessage(mappingConfig, messageEvent);
                messages.Add(slackMessage);
            }
            return messages as IReadOnlyList<SlackMessage>;
        }

        private SlackMessage CreateSlackMessage(ChannelMappingConfig mappingConfig, MessageEvent messageEvent)
        {
            switch(messageEvent.Message.Type)
            {
                case EventMessageType.Text:

                    return new SlackMessage {
                        Channel = mappingConfig.SlackChannelId,
                        Text = AttatchMention((messageEvent.Message as TextEventMessage)?.Text),
                        Username = "木下賢也"
                    };
                default:
                    throw new Exception($"無効なLine Webhook Event {messageEvent.Message.Type} が指定されました。");
            }
            string AttatchMention(string text) => $"<@U0226MT50F6>\n{text}";
        }
    }
}
