using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Models;
using System.Threading.Tasks;
using Line.Messaging.Webhooks;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Services
{
    public interface IChannelMappingConfigService
    {
        public Task<ChannelMappingConfig> MakeChannelMappingConfigAsync(string botId, FollowEvent followEvent);

        public Task DeleteChannelMappingConfigAsync(string botId, UnfollowEvent unfollowEvent);
    }
}
