using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Services
{
    public class ChannelMappingService: IChannelMappingService
    {
        private static readonly Dictionary<string, string> _channelMapper = new Dictionary<string, string> {
        };

        public ChannelSwitchEntity GetWithLineChannel(string lineChannelId) {
            return new ChannelSwitchEntity { };
        }

        public ChannelSwitchEntity GetWithSlackChannel(string slackChannelId)
        {
            return new ChannelSwitchEntity { };
        }
    }
}
