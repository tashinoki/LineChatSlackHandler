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
        private readonly CloudTable _channelMappingConfigurationsTable;

        public ChannelMappingConfigRepository()
        {
            var account = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("TableStorage"));
            var tableClient = account.CreateCloudTableClient();
            _channelMappingConfigurationsTable = tableClient.GetTableReference("ChannelMappingConfigurations");
        }

        public async Task<ChannelMappingConfig> GetWithLineUserIdAsync(string userId, string botId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(botId))
                throw new ArgumentException("LineUserId もしくは LineBotId が空です");


            var operation = TableOperation.Retrieve<ChannelMappingConfig>(botId, userId);

            try
            {
                var result = await _channelMappingConfigurationsTable.ExecuteAsync(operation);

                if (result.HttpStatusCode >= 400 && result.HttpStatusCode <= 500)
                    throw new Exception("チャンネルの設定が見つかりませんでした。");

                var config = result.Result as ChannelMappingConfig;

                if (config is null)
                    throw new Exception("");

                return config;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<ChannelMappingConfig> GetWithSlackChannelIdAsync(string channelId)
        {
            if (string.IsNullOrEmpty(channelId))
                throw new ArgumentException("不正な Channel Id です");

            var query = new TableQuery<ChannelMappingConfig>().Where(
                TableQuery.GenerateFilterCondition(nameof(ChannelMappingConfig.SlackChannelId), QueryComparisons.Equal, channelId));
            var configs = new List<ChannelMappingConfig>();

            TableContinuationToken token = null;
            try
            {
                do
                {
                    var querySegment = await _channelMappingConfigurationsTable.ExecuteQuerySegmentedAsync(query, token);
                    configs.AddRange(querySegment.Results);

                    if (querySegment.Count() > 1)
                        throw new Exception("チャンネルが複数あります。");

                    token = querySegment.ContinuationToken;
                } while (token != null);

                return configs.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
