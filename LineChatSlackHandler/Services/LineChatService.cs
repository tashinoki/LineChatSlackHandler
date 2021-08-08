using System;
using System.Collections.Generic;
using System.Text;
using LineChatSlackHandler.Entity;
using LineChatSlackHandler.Models;
using System.Threading.Tasks;
using Line.Messaging;

namespace LineChatSlackHandler.Services
{
    public class LineChatService: ILineChatService
    {
        private readonly string _lineAccessToken = Environment.GetEnvironmentVariable("LineAccessToken");

        private readonly string _lineSecretToken = Environment.GetEnvironmentVariable("LineSecretToken");

        private readonly static LineMessagingClient _messagingClient = new LineMessagingClient(
                Environment.GetEnvironmentVariable("LineAccessToken")
            );

        public async Task SendMessageAsync(SlackEvent slackEvent, ChannelSwitchEntity channelSwitch)
        {
        }
    }
}
