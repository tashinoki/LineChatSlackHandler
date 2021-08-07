using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LineChatSlackHandler.Entity;

namespace LineChatSlackHandler.Services
{
    public class ChannelMappingService: IChannelMappingService
    {
        public ChannelMappingService()
        { }

        // { "SlackChannel" => "LineChannel" }
        // Todo
        // InMemoryかTableStorageに書き換える
        private static readonly Dictionary<string, string> _channelMapper = new Dictionary<string, string> {
            { "C02B6KW4K4G", "hogehoge" }
        };

        public ChannelSwitchEntity GetWithLineChannel(string lineChannelId)
        {

            if (lineChannelId is null || string.IsNullOrWhiteSpace(lineChannelId))
                throw new ArgumentNullException("Line Channle Idが NULL もしくはからの値です。");

            var slackCannelId = _channelMapper.FirstOrDefault(x => x.Value.Contains(lineChannelId)).Key;

            if (slackCannelId is null)
                throw new Exception("");

            return new ChannelSwitchEntity
            {
                SlackChannelId = slackChannelId,
                LineChannelId = lineChannelId

            };
        }

        public ChannelSwitchEntity GetWithSlackChannel(string slackChannelId)
        {
            if (slackChannelId is null || string.IsNullOrWhiteSpace(slackChannelId))
                throw new ArgumentNullException("Slack Channle Idが NULL もしくはからの値です。");

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
