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

        // { "SlackChannel" => "LineUserId" }
        // Todo
        // InMemoryかTableStorageに書き換える
        private static readonly Dictionary<string, string> _channelMapper = new Dictionary<string, string> {
            { "C02B6KW4K4G", "U57853f60cf6cbc8db966086785a9f591" }
        };

        public ChannelMappingConfig GetWithLineChannel(string lineChannelId)
        {

            if (lineChannelId is null || string.IsNullOrWhiteSpace(lineChannelId))
                throw new ArgumentNullException("Line Channle Idが NULL もしくはからの値です。");

            var slackChannelId = _channelMapper.FirstOrDefault(x => x.Value.Contains(lineChannelId)).Key;

            if (slackChannelId is null)
                throw new Exception($"Line Channel Id: {lineChannelId}に対応する Slack Channel Idはありません。");

            return new ChannelMappingConfig
            {
                SlackChannelId = slackChannelId,
                LineUserId = lineChannelId

            };
        }

        public ChannelMappingConfig GetWithSlackChannel(string slackChannelId)
        {
            if (slackChannelId is null || string.IsNullOrWhiteSpace(slackChannelId))
                throw new ArgumentNullException("Slack Channle Idが NULL もしくはからの値です。");

            if (_channelMapper.TryGetValue(slackChannelId, out var lineUserId))
            {
                return new ChannelMappingConfig
                {
                    SlackChannelId = slackChannelId,
                    LineUserId = lineUserId
                };
            }

            throw new Exception($"Slack Channel Id: {slackChannelId}に対応するLine Channel Idがありません。");
        }
    }
}
