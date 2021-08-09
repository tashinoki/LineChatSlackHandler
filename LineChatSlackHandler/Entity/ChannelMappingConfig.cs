using System;
using System.Collections.Generic;
using System.Text;

namespace LineChatSlackHandler.Entity
{
    public class ChannelMappingConfig
    {
        public string SlackChannelId { get; set; }

        public string LineUserId { get; set; }

        public string LineBotId { get; set; }
    }
}
