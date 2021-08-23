using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LineChatSlackHandler.Models;

namespace LineChatSlackHandler.Services
{
    public interface ISlackChannelService
    {
        public Task<Channel> StartConversationAsync(string lineUserId);
    }
}
