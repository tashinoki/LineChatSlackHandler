using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Repository
{
    public interface IChannelMappingConfigRepository
    {
        public ChannelMappingConfig GetWithLineUserId(string userId, string botId);

        public ChannelMappingConfig GetWithSlackChannelId(string channelId);
    }
}
