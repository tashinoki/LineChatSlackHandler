using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LineChatSlackHandler.Models;

namespace LineChatSlackHandler.Services
{
    public interface ISlackApiClient
    {
        public Task<IEnumerable<Channel>> FetchChannelListAsync();

        public Task<Channel> CreateChannelAsync(string name);
    }
}
