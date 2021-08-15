using System;
using System.Collections.Generic;
using System.Linq;
using LineChatSlackHandler.Entity;
using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;
using System.Net;

namespace LineChatSlackHandler.Repository
{
    public class ChannelMappingConfigRepository: IChannelMappingConfigRepository
    {            
        // { "SlackChannel" => "LineUserId" }
        // Todo
        // InMemoryかTableStorageに書き換える
        private static readonly Dictionary<string, string> _mappingConfigStorage = new Dictionary<string, string> {
            { "U84fcdf3e51332276d682851b85caf682$C02B6KW4K4G", "U57853f60cf6cbc8db966086785a9f591" }
        };

        private readonly CloudTable _channelMappingConfigurationsTable;

        public ChannelMappingConfigRepository()
        {
            var account = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("TableStorage"));
            var tableClient = account.CreateCloudTableClient();
            _channelMappingConfigurationsTable = tableClient.GetTableReference("ChannelMappingConfigurations");
        }

        public async Task<ChannelMappingConfig> GetWithLineUserId(string userId, string botId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(botId))
                throw new ArgumentException("");


            var operation = TableOperation.Retrieve<ChannelMappingConfig>(botId, userId);
            var result =await _channelMappingConfigurationsTable.ExecuteAsync(operation);

            if (result.HttpStatusCode >= 400 && result.HttpStatusCode <= 500)
                throw new Exception("チャンネルの設定が見つかりませんでした。");

            var config = result.Result as ChannelMappingConfig;

            if (config is null)
                throw new Exception("");

            return config;
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
