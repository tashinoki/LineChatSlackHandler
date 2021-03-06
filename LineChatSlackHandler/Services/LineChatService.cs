using System;
using System.Collections.Generic;
using LineChatSlackHandler.Models;
using System.Threading.Tasks;
using Line.Messaging;

namespace LineChatSlackHandler.Services
{
    public class LineChatService: ILineChatService
    {
        private readonly static LineMessagingClient _messagingClient = new LineMessagingClient(
                Environment.GetEnvironmentVariable("LineAccessToken")
            );

        public async Task SendMessageAsync(LineMessage message)
        {
            _messagingClient.PushMessageAsync(message.ToUserId,  new List<ISendMessage> { message.Message });
        }
    }
}
