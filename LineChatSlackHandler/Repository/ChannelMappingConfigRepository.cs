using System;
using System.Collections.Generic;
using System.Linq;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Repository
{
    public class ChannelMappingConfigRepository: IChannelMappingConfigRepository
    {            
        // { "SlackChannel" => "LineUserId" }
        // Todo
        // InMemoryかTableStorageに書き換える
        private static readonly Dictionary<string, string> _mappingConfigStorage = new Dictionary<string, string> {
            { "C02B6KW4K4G", "U57853f60cf6cbc8db966086785a9f591" }
        };

        public ChannelMappingConfig GetWithLineUserId(string userId, string botId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(botId))
                throw new ArgumentException("");

            var slackChannelId = _mappingConfigStorage.FirstOrDefault(x => x.Value.Equals(userId)).Key;

            if (slackChannelId is null)
                throw new Exception("");

            return new ChannelMappingConfig
            {
                SlackChannelId = slackChannelId,
                LineUserId = userId,
                LineBotId = botId
            };
        }

        public ChannelMappingConfig GetWithSlackChannelId(string channelId, string botId)
        {
            throw new Exception();
        }
    }
}
