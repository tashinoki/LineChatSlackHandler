using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Services
{
    public interface IChannelMappingService
    {
        public ChannelMappingConfig GetWithLineChannel(string lineChannelId);

        public ChannelMappingConfig GetWithSlackChannel(string slackChannelId);
    }
}
