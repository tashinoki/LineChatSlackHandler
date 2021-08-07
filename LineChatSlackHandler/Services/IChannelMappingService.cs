using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Services
{
    public interface IChannelMappingService
    {
        public ChannelSwitchEntity GetWithLineChannel(string lineChannelId);

        public ChannelSwitchEntity GetWithSlackChannel(string slackChannelId);
    }
}
