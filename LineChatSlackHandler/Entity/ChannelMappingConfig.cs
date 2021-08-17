using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos.Table;

namespace LineChatSlackHandler.Entity
{
    public class ChannelMappingConfig: TableEntity
    {
        public string SlackChannelId { get; set; }

        public string LineUserId { get; set; }

        public string LineBotId { get; set; }
    }
}
