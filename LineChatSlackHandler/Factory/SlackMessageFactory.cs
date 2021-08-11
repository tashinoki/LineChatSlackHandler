using Line.Messaging.Webhooks;
using LineChatSlackHandler.Entity;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Repository;
using System;
using System.Collections.Generic;

namespace LineChatSlackHandler.Factory
{
    public class SlackMessageFactory: IChannelMessageFactory
    {
        private readonly IChannelMappingConfigRepository _mappingConfigRepository;

        public SlackMessageFactory(IChannelMappingConfigRepository channelMappingConfigRepository)
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
