using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Services
{
    public class ChannelMappingService: IChannelMappingService
    {
        public ChannelMappingService()
        { }

        // { "SlackChannel" => "LineChannel" }
        private static readonly Dictionary<string, string> _channelMapper = new Dictionary<string, string> {
            { "C02B6KW4K4G", "hogehoge" }
        };

        public ChannelSwitchEntity GetWithLineChannel(string lineChannelId) {
            return new ChannelSwitchEntity { };
        }

        public ChannelSwitchEntity GetWithSlackChannel(string slackChannelId)
        {
            if (_channelMapper.TryGetValue(slackChannelId, out var lineChannelId))
            {
                return new ChannelSwitchEntity
                {
                    SlackChannelId = slackChannelId,
                    LineChannelId = lineChannelId
                };
            }

            throw new Exception($"Slack Channel Id: {slackChannelId}に対応するLine Channel Idがありません。");
        }
    }
}
