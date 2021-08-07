using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LineChatSlackHandler.Entity;
using LineChatSlackHandler.Models;

namespace LineChatSlackHandler.Services
{
    public interface ILineChatService
    {
        public Task SendMessageAsync(SlackEvent slackEvent, ChannelSwitchEntity channelSwitch);
    }
}
