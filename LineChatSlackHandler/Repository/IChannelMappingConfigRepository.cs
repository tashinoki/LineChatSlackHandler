using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Repository
{
    public interface IChannelMappingConfigRepository
    {
        public Task<ChannelMappingConfig> GetWithLineUserIdAsync(string userId, string botId);

        public Task<ChannelMappingConfig> GetWithSlackChannelIdAsync(string channelId);

        public Task Create(string botId, string userId);
    }
}
