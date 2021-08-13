using System;
using System.Collections.Generic;
using System.Linq;
using LineChatSlackHandler.Entity;
using Microsoft.Azure.Cosmos.Table;

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

        private readonly CloudTable _channelMappingConfigurationsTable;

        public ChannelMappingConfigRepository()
        {
            var account = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("TableStorage"));
            var tableClient = account.CreateCloudTableClient();
            _channelMappingConfigurationsTable = tableClient.GetTableReference("ChannelMappingConfigurations");
        }

        public ChannelMappingConfig GetWithLineUserId(string userId, string botId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(botId))
                throw new ArgumentException("");

            var slackChannelId = _mappingConfigStorage.FirstOrDefault(x => x.Value.Equals($"{botId}${userId}")).Key;

            if (slackChannelId is null)
                throw new Exception("");

            return new ChannelMappingConfig
            {
                SlackChannelId = slackChannelId,
                LineUserId = userId,
                LineBotId = botId
            };
        }

        public ChannelMappingConfig GetWithSlackChannelId(string channelId)
        {
            if (string.IsNullOrEmpty(channelId))
                throw new ArgumentException("不正な Channel Id です");

            if (!_mappingConfigStorage.TryGetValue(channelId, out var lineUserId))
            
                throw new Exception("登録されていないチャンネル ID です");

            return new ChannelMappingConfig
            {
                LineBotId = "",
                LineUserId = lineUserId,
                SlackChannelId = channelId,
            };
        }
    }
}
