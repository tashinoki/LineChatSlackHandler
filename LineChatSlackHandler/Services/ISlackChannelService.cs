using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LineChatSlackHandler.Services
{
    public interface ISlackChannelService
    {
        public Task<IEnumerable<string>> StartConversationAsync(string channelId);
    }
}
