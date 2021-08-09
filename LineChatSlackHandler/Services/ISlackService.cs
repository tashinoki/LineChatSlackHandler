using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LineChatSlackHandler.Models;

namespace LineChatSlackHandler.Services
{
    public interface ISlackService
    {
        public Task SendMessagesAsync(IReadOnlyList<SlackMessage> slackMessages);
    }
}
