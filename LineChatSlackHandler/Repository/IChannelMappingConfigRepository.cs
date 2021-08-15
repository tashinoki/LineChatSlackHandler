using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Repository
{
    public interface IChannelMappingConfigRepository
    {
        public Task<ChannelMappingConfig> GetWithLineUserId(string userId, string botId);

        public ChannelMappingConfig GetWithSlackChannelId(string channelId);
    }
}
